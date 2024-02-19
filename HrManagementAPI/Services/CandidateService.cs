using HrManagementAPI.DTOs;
using HrManagementAPI.Mappers;
using HrManagementAPI.Models;
using HrManagementAPI.QueryParameters;
using HrManagementAPI.Types;
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

            if (!string.IsNullOrEmpty(candidateParameters.first_name))
                candidates = candidates.Where(x => x.FirstName == candidateParameters.first_name);

            if (!string.IsNullOrEmpty(candidateParameters.last_name))
                candidates = candidates.Where(x => x.LastName == candidateParameters.last_name);

            if (candidateParameters.status != null)
                candidates = candidates.Where(x => x.Status == candidateParameters.status);

            return await candidates.Select(x => _mapper.CandidateToDto(x)).ToListAsync();
        }

        public async Task<DtoCandidate> GetCandidateByIdAsync(int candidateId)
        {
            return await _context.Candidates
                .Where(x => x.CandidateId == candidateId)
                .Select(x => _mapper.CandidateToDto(x))
                .AsQueryable()
                .FirstOrDefaultAsync();
        }

        public async Task<DtoCandidate> AddCandidate(DtoCreateCandidate candidateInfo)
        {
            if (_context.Candidates.Where(x => x.FirstName == candidateInfo.FirstName &&
                                               x.LastName == candidateInfo.LastName &&
                                               x.BirthDate == candidateInfo.BirthDate &&
                                               x.PhoneNumber == candidateInfo.PhoneNumber &&
                                               x.Email == candidateInfo.Email &&
                                               x.Status == candidateInfo.Status).FirstOrDefault() != null)
            {
                throw new ArgumentException("Candidate with identical data already exists");
            }

            if (!string.IsNullOrEmpty(candidateInfo.Email) && _context.Candidates.Where(x => x.Email == candidateInfo.Email).FirstOrDefault() != null)
            {
                throw new ArgumentException("Entered email is already assigned to a candidate");
            }
            if (!string.IsNullOrEmpty(candidateInfo.PhoneNumber) && _context.Candidates.Where(x => x.PhoneNumber == candidateInfo.PhoneNumber).FirstOrDefault() != null)
            {
                throw new ArgumentException("Entered phone number is already assigned to a candidate");
            }

            Candidate newCandidate = _mapper.DtoToCandidate(candidateInfo);
            _context.Candidates.Add(newCandidate);
            await _context.SaveChangesAsync();

            return _mapper.CandidateToDto(newCandidate);
        }

        public async Task<DtoCandidate> UpdateCandidate(int candidateId, DtoCreateCandidate candidateInfo)
        {
            var candidate = await _context.Candidates.Where(x => x.CandidateId == candidateId).FirstOrDefaultAsync();
            if (candidate == null)
            {
                throw new ArgumentException("Candidate not found", nameof(candidateId));
            }

            if (_context.Candidates.Where(x => x.FirstName == candidateInfo.FirstName &&
                                               x.LastName == candidateInfo.LastName &&
                                               x.BirthDate == candidateInfo.BirthDate &&
                                               x.PhoneNumber == candidateInfo.PhoneNumber &&
                                               x.Email == candidateInfo.Email &&
                                               x.Status == candidateInfo.Status).SingleOrDefault() != null)
            {
                throw new ArgumentException("Candidate with identical data already exists");
            }

            if (!string.IsNullOrEmpty(candidate.Email) && _context.Candidates.Where(x => x.Email == candidateInfo.Email).FirstOrDefault() != null)
            {
                throw new ArgumentException("Entered email is already assigned to a candidate");
            }
            if (!string.IsNullOrEmpty(candidate.PhoneNumber) && _context.Candidates.Where(x => x.PhoneNumber == candidateInfo.PhoneNumber).FirstOrDefault() != null)
            {
                throw new ArgumentException("Entered phone number is already assigned to a candidate");
            }

            candidate.FirstName = candidateInfo.FirstName;
            candidate.LastName = candidateInfo.LastName;
            candidate.BirthDate = candidateInfo.BirthDate;
            candidate.PhoneNumber = candidateInfo.PhoneNumber;
            candidate.Email = candidateInfo.Email;
            candidate.Status = candidateInfo.Status;

            var rowCount = await _context.SaveChangesAsync();

            return (_mapper.CandidateToDto(candidate));
        }

        public async Task DeleteCandidate(int candidateId)
        {
            _context.Candidates.Remove(_context.Candidates.First(x => x.CandidateId == candidateId));
            await _context.SaveChangesAsync();
        }
    }
}
