using HrManagementAPI.DTOs;
using HrManagementAPI.Models;
using HrManagementAPI.Models.RootParameters;
using HrManagementAPI.ModelsMainInfo;

namespace HrManagementAPI.Services
{
    public interface ISubmissionService
    {
        public Task<List<DtoSubmission>> GetSubmissionsAsync(SubmissionParameters submissionParameters);

        public Task<DtoSubmission> GetSubmissionByIdAsync(int subId);

        public Task<DtoSubmission> AddSubmissionAsync(DtoSubmissionCreate newSubmission);

        public Task<DtoSubmission> UpdateSubmissionAsync(int subId, DtoSubmissionCreate updSubmission);

        public Task DeleteSubmissionAsync(int subiId);
    }
}
