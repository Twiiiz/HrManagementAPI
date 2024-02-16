using HrManagementAPI.Models;
using HrManagementAPI.ModelsMainInfo;
using HrManagementAPI.QueryParameters;
using HrManagementAPI.Types;
using Microsoft.EntityFrameworkCore;

namespace HrManagementAPI.Repositories
{
    public class CandidateService: ICandidateService
    {
        private readonly HrManagementContext _context;

        public CandidateService(HrManagementContext context)
        {
            _context = context;
        }

        public async Task<List<Candidate>> GetCandidatesAsync(CandidateParameters candidate_parameters)
        {
            var candidates = _context.Candidates.AsQueryable();

            if (candidates.Any())
            {
                if (!string.IsNullOrEmpty(candidate_parameters.first_name))
                    candidates = candidates.Where(x => x.FirstName.ToLower() == candidate_parameters.first_name.ToLower());

                if (!string.IsNullOrEmpty(candidate_parameters.last_name))
                    candidates = candidates.Where(x => x.LastName.ToLower() == candidate_parameters.last_name.ToLower());

                if (candidate_parameters.status != null)
                {
                    var status = candidate_parameters.status;

                    switch (status)
                    {
                        case CandidateStatus.hired:
                            candidates = candidates.Where(x => x.Status == CandidateStatus.hired);

                            break;

                        case CandidateStatus.not_hired:
                            candidates = candidates.Where(x => x.Status == CandidateStatus.not_hired);

                            break;

                        case CandidateStatus.offer_made:
                            candidates = candidates.Where(x => x.Status == CandidateStatus.offer_made);

                            break;

                        case CandidateStatus.offer_denied:
                            candidates = candidates.Where(x => x.Status == CandidateStatus.offer_denied);

                            break;

                        case CandidateStatus.spam:
                            candidates = candidates.Where(x => x.Status == CandidateStatus.spam);

                            break;
                    }
                }
            }

            return await candidates.ToListAsync();
        }

        public async Task<Candidate> GetCandidateByIdAsync(int candidate_id)
        {
            return await _context.Candidates
                .Where(x => x.CandidateId == candidate_id)
                .AsQueryable()
                .FirstOrDefaultAsync();
        }

        public async Task<Candidate> AddCandidate(CandidateMainInfo candidate_info)
        {
            Candidate new_candidate = new Candidate()
            {
                CandidateId = default,
                FirstName = candidate_info.FirstName,
                LastName = candidate_info.LastName,
                BirthDate = candidate_info.BirthDate,
                PhoneNumber = candidate_info.PhoneNumber,
                Email = candidate_info.Email,
                Status = (CandidateStatus)candidate_info.Status
            };

            _context.Candidates.Add(new_candidate);

            await _context.SaveChangesAsync();

            return new_candidate;
        }

        public async Task<Candidate> UpdateCandidate(int candidate_id, CandidateMainInfo candidate_info)
        {
            var candidate = await _context.Candidates.Where(x => x.CandidateId == candidate_id).FirstOrDefaultAsync();
            if (candidate == null)
            {
                throw new ArgumentException("Candidate not found", nameof(candidate_id));
            }
            else
            {
                candidate.FirstName = candidate_info.FirstName;
                candidate.LastName = candidate_info.LastName;
                candidate.BirthDate = candidate_info.BirthDate;
                candidate.PhoneNumber = candidate_info.PhoneNumber;
                candidate.Email = candidate_info.Email;
                candidate.Status = (CandidateStatus)candidate_info.Status;

                await _context.SaveChangesAsync();

                return (candidate);
            }
        }

        public async Task<bool> DeleteCandidate(int candidate_id)
        {
            var candidate = await _context.Candidates.Where(x => x.CandidateId == candidate_id).FirstOrDefaultAsync();
            if (candidate == null)
                return false;

            _context.Candidates.Remove(_context.Candidates.First(x => x.CandidateId == candidate_id));
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
