using HrManagementAPI.DTOs;
using HrManagementAPI.Mappers;
using HrManagementAPI.Models;
using HrManagementAPI.Models.RootParameters;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace HrManagementAPI.Repositories
{
    public class CandidateService: ICandidateService
    {
        private readonly Mapper _mapper;
        private readonly HrManagementContext _context;

        public CandidateService(HrManagementContext context)
        {
            _context = context;
            _mapper = new Mapper();
        }

        public async Task<List<DtoCandidate>> GetCandidatesAsync(CandidateParameters candidateParameters)
        {
            var candidates = _context.Candidates.AsQueryable();

            if (!string.IsNullOrEmpty(candidateParameters.FirstName))
                candidates = candidates.Where(x => x.FirstName == candidateParameters.FirstName);

            if (!string.IsNullOrEmpty(candidateParameters.LastName))
                candidates = candidates.Where(x => x.LastName == candidateParameters.LastName);

            if (candidateParameters.Status != null)
                candidates = candidates.Where(x => x.Status == candidateParameters.Status);

            return await candidates.Select(x => _mapper.EntityToDto(x)).ToListAsync();
        }

        public async Task<DtoCandidate> GetCandidateByIdAsync(int candidateId)
        {
            return await _context.Candidates
                .Where(x => x.CandidateId == candidateId)
                .Select(x => _mapper.EntityToDto(x))
                .AsQueryable()
                .FirstOrDefaultAsync();
        }

        public async Task<DtoCandidate> AddCandidateAsync(DtoCandidateCreate candidateInfo)
        {
            if (!await IsUnique(candidateInfo))
            {
                throw new ArgumentException("Candidate with identical data already exists");
            }

            if (!string.IsNullOrEmpty(candidateInfo.Email) && _context.Candidates.Where(x =>
            x.Email == candidateInfo.Email).FirstOrDefault() != null)
            {
                throw new ArgumentException("Entered email is already assigned to a candidate");
            }
            if (!string.IsNullOrEmpty(candidateInfo.PhoneNumber) && _context.Candidates.Where(x =>
            x.PhoneNumber == candidateInfo.PhoneNumber).FirstOrDefault() != null)
            {
                throw new ArgumentException("Entered phone number is already assigned to a candidate");
            }

            Candidate newCandidate = _mapper.DtoToEntity(candidateInfo);
            _context.Candidates.Add(newCandidate);
            await _context.SaveChangesAsync();

            return _mapper.EntityToDto(newCandidate);
        }

        public async Task<DtoCandidate> UpdateCandidateAsync(int candidateId, DtoCandidateCreate candidateInfo)
        {
            var candidate = await _context.Candidates.Where(x =>
            x.CandidateId == candidateId).FirstOrDefaultAsync();

            if (!await IsUnique(candidateInfo))
            {
                throw new ArgumentException("Candidate with identical data already exists");
            }

            if (!string.IsNullOrEmpty(candidate.Email) && _context.Candidates.Where(x
                => x.Email == candidateInfo.Email).FirstOrDefault() != null)
            {
                throw new ArgumentException("Entered email is already assigned to a candidate");
            }
            if (!string.IsNullOrEmpty(candidate.PhoneNumber) && _context.Candidates.Where(x =>
            x.PhoneNumber == candidateInfo.PhoneNumber).FirstOrDefault() != null)
            {
                throw new ArgumentException("Entered phone number is already assigned to a candidate");
            }

            candidate.FirstName = candidateInfo.FirstName;
            candidate.LastName = candidateInfo.LastName;
            candidate.BirthDate = candidateInfo.BirthDate;
            candidate.PhoneNumber = candidateInfo.PhoneNumber;
            candidate.Email = candidateInfo.Email;
            candidate.Status = candidateInfo.Status;

            await _context.SaveChangesAsync();

            return _mapper.EntityToDto(candidate);
        }

        public async Task DeleteCandidateAsync(int candidateId)
        {
            _context.Candidates.Remove(_context.Candidates.First(x => x.CandidateId == candidateId));
            await _context.SaveChangesAsync();
        }

        private async Task<bool> IsUnique(DtoCandidateCreate entryCandidate)
        {
            var candidate = await _context.Candidates.Where(x => x.FirstName == entryCandidate.FirstName &&
                                               x.LastName == entryCandidate.LastName &&
                                               x.BirthDate == entryCandidate.BirthDate &&
                                               x.PhoneNumber == entryCandidate.PhoneNumber &&
                                               x.Email == entryCandidate.Email &&
                                               x.Status == entryCandidate.Status).FirstOrDefaultAsync();
            if (candidate != null)
                return false;

            return true;
        }
    }
}
