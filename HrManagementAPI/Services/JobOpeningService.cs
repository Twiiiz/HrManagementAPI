using HrManagementAPI.DTOs;
using HrManagementAPI.Mappers;
using HrManagementAPI.Models;
using HrManagementAPI.Models.RootParameters;
using Microsoft.EntityFrameworkCore;

namespace HrManagementAPI.Services
{
    public class JobOpeningService: IJobOpeningService
    {
        private readonly HrManagementContext _context;
        private readonly Mapper _mapper;

        public JobOpeningService(HrManagementContext context)
        {
            _context = context;
            _mapper = new Mapper();
        }

        public async Task<List<DtoJobOpening>> GetJobOpeningsAsync(JobOpeningParameters parameters)
        {
            var jobOpenings = _context.JobOpenings.AsQueryable();

            if (parameters.OfficeId != null)
                jobOpenings = jobOpenings.Where(x => x.OfficeId == parameters.OfficeId);

            if (parameters.PositionId != null)
                jobOpenings = jobOpenings.Where(x => x.PositionId == parameters.PositionId);

            if (parameters.OpeningDate != null)
                jobOpenings = jobOpenings.Where(x => x.OpeningDate ==  parameters.OpeningDate);

            if (parameters.PositionId != null)
                jobOpenings = jobOpenings.Where(x => x.PositionId == parameters.PositionId);

            if (parameters.Status != null)
                jobOpenings = jobOpenings.Where(x => x.Status == parameters.Status);

            if (parameters.HiredCandidate != null)
                jobOpenings = jobOpenings.Where(x => x.HiredCandidate == parameters.HiredCandidate);

            return await jobOpenings.Select(x => _mapper.EntityToDto(x)).ToListAsync();
        }

        public async Task<DtoJobOpening> GetJobOpeningByIdAsync(int openingId)
        {
            var jobOpening = await _context.JobOpenings.Where(x => x.OpeningId == openingId).FirstOrDefaultAsync();
            if (jobOpening == null)
                throw new ArgumentException("Unable to fing requested job opening");

            return _mapper.EntityToDto(jobOpening);
        }

        public async Task<DtoJobOpening> AddJobOpeningAsync(DtoJobOpeningCreate jobOpeningInfo)
        {
            var jobOpening = _mapper.DtoToEntity(jobOpeningInfo);
            jobOpening.OpeningDate = DateOnly.FromDateTime(DateTime.Today);
            _context.JobOpenings.Add(jobOpening);
            await _context.SaveChangesAsync();

            return _mapper.EntityToDto(jobOpening);
        }

        public async Task<DtoJobOpening> UpdateJobOpeningAsync(int openingId, DtoJobOpeningCreate jobOpeningInfo)
        {
            var jobOpening = await _context.JobOpenings.Where(x => x.OpeningId == openingId).FirstOrDefaultAsync();
            if (jobOpening == null)
                throw new ArgumentException("Unable to find requested job opening");

            jobOpening.OfficeId = jobOpeningInfo.OfficeId;
            jobOpening.PositionId = jobOpeningInfo.PositionId;
            jobOpening.OpeningDate = jobOpening.OpeningDate;
            jobOpening.LastUpdateDate = DateOnly.FromDateTime(DateTime.Now);
            jobOpening.Status = jobOpeningInfo.Status;
            jobOpening.HiredCandidate = jobOpeningInfo.HiredCandidate;
            await _context.SaveChangesAsync();

            return _mapper.EntityToDto(jobOpening);
        }

        public async Task DeleteJobOpeningAsync(int openingId)
        {
            var jobOpening = _context.JobOpenings.Where(x => x.OpeningId == openingId).First();

            _context.JobOpenings.Remove(jobOpening);
            await _context.SaveChangesAsync();
        }
    }
}
