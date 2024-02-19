using HrManagementAPI.Models;
using HrManagementAPI.DTOs;
using HrManagementAPI.Models.RootParameters;

namespace HrManagementAPI.Repositories
{
    public interface ICandidateService
    {
        public Task<List<DtoCandidate>> GetCandidatesAsync(CandidateParameters candidateParameters);

        public Task<DtoCandidate> GetCandidateByIdAsync(int candidateId);

        public Task<DtoCandidate> AddCandidateAsync(DtoCandidateCreate newCandidate);

        public Task<DtoCandidate> UpdateCandidateAsync(int candidateId, DtoCandidateCreate updCandidate);

        public Task DeleteCandidateAsync(int candidateId);
    }
}
