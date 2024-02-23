using HrManagementAPI.DTOs;
using HrManagementAPI.Mappers;
using HrManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HrManagementAPI.Services
{
    public class SubmissionStatusService: ISubmissionStatusService
    {
        private readonly HrManagementContext _context;
        private readonly Mapper _mapper;

        public SubmissionStatusService(HrManagementContext context)
        {
            _context = context;
            _mapper = new Mapper();
        }

        public async Task<List<DtoSubmissionStatus>> GetSubmissionStatusesAsync(int subId, int hrId)
        {
            var submission = await GetSubByIdAsync(subId, hrId);

            return await _context.SubmissionStatuses.Where(x => x.SubId ==  submission.SubId).Select(x => _mapper.EntityToDto(x)).ToListAsync();
        }

        public async Task<DtoSubmissionStatus> GetSubmissionStatusByIdAsync(int subId, int subStatId)
        {
            var submissionStatus = await _context.SubmissionStatuses
                .Where(x => x.SubId == subId)
                .Where(x => x.SubStatId ==  subStatId)
                .Select(x => _mapper.EntityToDto(x))
                .FirstOrDefaultAsync();
            if (submissionStatus == null)
                throw new ArgumentException("Unable to find requested status for provided submission");

            return submissionStatus;
        }

        public async Task<DtoSubmissionStatus> AddSubmissionStatusAsync(int subId, DtoSubmissionStatusCreate submissionStatusInfo)
        {
            if (!await IsUnique(subId, submissionStatusInfo))
                throw new ArgumentException("Identical status already exists");

            SubmissionStatus submissionStatus = _mapper.DtoToEntity(submissionStatusInfo, subId);
            _context.SubmissionStatuses.Add(submissionStatus);

            return _mapper.EntityToDto(submissionStatus);
        }

        public async Task<DtoSubmissionStatus> UpdateSubmissionStatusAsync(int subId, int subStatId, DtoSubmissionStatusCreate submissionStatusInfo)
        {
            if (!await IsUnique(subStatId, submissionStatusInfo))
                throw new ArgumentException("Identical status already exists");

            var submissionStatus = _mapper.DtoToEntity(submissionStatusInfo, subId);
            _context.SubmissionStatuses.Add(submissionStatus);
            await _context.SaveChangesAsync();
            return _mapper.EntityToDto(submissionStatus);
        }

        public async Task DeleteSubmissionStatusAsync(int subId, int subStatId, int hrId)
        {
            var submission = GetSubByIdAsync(subId, hrId);
            var dtoSubmissionStatus = await GetSubmissionStatusByIdAsync(subId, subStatId);
            if (dtoSubmissionStatus == null)
                throw new ArgumentException("Unable to find requested status for provided submission");
            var submissionStatus = new SubmissionStatus
            {
                SubStatId = dtoSubmissionStatus.SubStatId,
                SubId = dtoSubmissionStatus.SubId,
                StatusName = dtoSubmissionStatus.StatusName,
                StatusDate = dtoSubmissionStatus.StatusDate
            };

            _context.SubmissionStatuses.Remove(submissionStatus);
            await _context.SaveChangesAsync();
        }
        
        private async Task<CandidateSubmission> GetSubByIdAsync(int subId, int hrId)
        {
            var submission = await _context.CandidateSubmissions.Where(x => x.SubId == subId).FirstOrDefaultAsync();
            if (submission == null)
                throw new ArgumentException("Unable to find requested submission");
            if (submission.HrId != hrId)
                throw new ArgumentException("Current HR manager doesn't have access to the requested submission");

            return submission;
        }

        private async Task<bool> IsUnique(int subId, DtoSubmissionStatusCreate submissionStatusInfo)
        {
            var submissionStatus = await _context.SubmissionStatuses.Where(x => x.SubId == subId &&
                                                               x.StatusName == submissionStatusInfo.StatusName &&
                                                               x.StatusDate == submissionStatusInfo.StatusDate).FirstOrDefaultAsync();
            if (submissionStatus != null)
                return false;

            return true;
        }
    }
}
