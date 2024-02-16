using HrManagementAPI.Models;
using HrManagementAPI.ModelsMainInfo;
using HrManagementAPI.QueryParameters;

namespace HrManagementAPI.Repositories
{
    public interface ICandidateService
    {
        public Task<List<Candidate>> GetCandidatesAsync(CandidateParameters candidate_parameters);

        public Task<Candidate> GetCandidateByIdAsync(int candidate_id);

        public Task<Candidate> AddCandidate(CandidateMainInfo new_candidate);

        public Task<Candidate> UpdateCandidate(int candidate_id, CandidateMainInfo upd_candidate);

        public Task<bool> DeleteCandidate(int candidate_id);
    }
}
