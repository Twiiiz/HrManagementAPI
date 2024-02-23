using HrManagementAPI.Mappers;
using HrManagementAPI.Models;
using HrManagementAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using HrManagementAPI.Models.RootParameters;

namespace HrManagementAPI.Services
{
    public class SubmissionService: ISubmissionService
    {
        private readonly Mapper _mapper;
        private readonly HrManagementContext _context;

        public SubmissionService(HrManagementContext context)
        {
            _context = context;
            _mapper = new Mapper();
        }

        public async Task<List<DtoSubmission>> GetSubmissionsAsync(int hrId, SubmissionParameters submissionParameters)
        {
            var submissions = _context.CandidateSubmissions.Where(x => x.HrId == hrId).AsQueryable();
            if (submissionParameters.CandidateId != null)
                submissions = submissions.Where(x => x.CandidateId == submissionParameters.CandidateId);

            if (submissionParameters.JobPosition != null)
                submissions = submissions.Where(x => x.JobPosition == submissionParameters.JobPosition);

            if (!string.IsNullOrEmpty(submissionParameters.PrefferredLocation))
                submissions = submissions.Where(x => x.PrefferredLocation.Contains(submissionParameters.PrefferredLocation));

            return await submissions.Select(x => _mapper.EntityToDto(x)).ToListAsync();
        }
        
        public async Task<DtoSubmission> GetSubmissionByIdAsync(int subId)
        {
            return await _context.CandidateSubmissions
                .Where(x => x.SubId == subId)
                .Select(x => _mapper.EntityToDto(x))
                .AsQueryable()
                .FirstOrDefaultAsync();
        }

        public async Task<DtoSubmission> AddSubmissionAsync(DtoSubmissionCreate submissionInfo)
        {
            if (!await IsUnique(submissionInfo))
                throw new ArgumentException("Submission With identical data already exists");
            
            CandidateSubmission submission = _mapper.DtoToEntity(submissionInfo);
            _context.CandidateSubmissions.Add(submission);
            await _context.SaveChangesAsync();

            return _mapper.EntityToDto(submission);
        }

        public async Task<DtoSubmission> UpdateSubmissionAsync(int subId, DtoSubmissionCreate submissionInfo)
        {
            if (!await IsUnique(submissionInfo))
                throw new ArgumentException("Submission with identical data already exists");

            var submission = await _context.CandidateSubmissions.Where(x => x.SubId == subId).FirstOrDefaultAsync();
            if (submission == null)
                throw new ArgumentException("Submission not found", nameof(subId));
            if (submissionInfo.HrId != submission.HrId)
                throw new ArgumentException("Provided HR manager doesn't have access to the submission");

            submission.CandidateId = submissionInfo.CandidateId;
            submission.SubDate = submissionInfo.SubDate;
            submission.JobPosition = submissionInfo.JobPosition;
            submissionInfo.CvFilepath = submissionInfo.CvFilepath;
            submissionInfo.HrId = submissionInfo.HrId;
            submissionInfo.PrefferredLocation = submissionInfo.PrefferredLocation;
            await _context.SaveChangesAsync();

            return _mapper.EntityToDto(submission);
        }

        public async Task DeleteSubmissionAsync(int subId, int hrId)
        {
            var submission = await _context.CandidateSubmissions.FirstAsync(x => x.SubId == subId);
            if (submission.HrId != hrId)
                throw new ArgumentException("Provided HR manager doesn't have access to the submission");

            _context.CandidateSubmissions.Remove(submission);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> IsUnique(DtoSubmissionCreate entrySubmission)
        {
            var submission = await _context.CandidateSubmissions.Where(x => x.CandidateId == entrySubmission.CandidateId &&
                                                           x.SubDate == entrySubmission.SubDate &&
                                                           x.JobPosition == entrySubmission.JobPosition &&
                                                           x.CvFilepath == entrySubmission.CvFilepath &&
                                                           x.HrId == entrySubmission.HrId &&
                                                           x.PrefferredLocation == entrySubmission.PrefferredLocation).FirstOrDefaultAsync();

            if (submission != null)
                return false;

            return true;
        }
    }
}
