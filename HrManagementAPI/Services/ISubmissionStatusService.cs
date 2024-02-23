using HrManagementAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HrManagementAPI.Services
{
    public interface ISubmissionStatusService
    {
        public Task<List<DtoSubmissionStatus>> GetSubmissionStatusesAsync(int subId, int hrId);

        public Task<DtoSubmissionStatus> GetSubmissionStatusByIdAsync(int subId, int subStatId); 

        public Task<DtoSubmissionStatus> AddSubmissionStatusAsync(int subId, DtoSubmissionStatusCreate submissionStatusInfo);

        public Task<DtoSubmissionStatus> UpdateSubmissionStatusAsync(int subId, int subStatId, DtoSubmissionStatusCreate submissionStatusInfo);

        public Task DeleteSubmissionStatusAsync(int subId, int subStatId, int hrId);
    }
}
