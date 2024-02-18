using HrManagementAPI.Models;
using HrManagementAPI.DTOs;
using HrManagementAPI.QueryParameters;

namespace HrManagementAPI.Repositories
{
    public interface ICandidateService
    {
        public Task<List<DtoCandidate>> GetCandidatesAsync(CandidateParameters candidateParameters);

        public Task<DtoCandidate> GetCandidateByIdAsync(int candidateId);

        public Task<DtoCandidate> AddCandidate(DtoCreateCandidate newCandidate);

        public Task<DtoCandidate> UpdateCandidate(int candidateId, DtoCreateCandidate updCandidate);

        public Task DeleteCandidate(int candidateId);
    }
}
