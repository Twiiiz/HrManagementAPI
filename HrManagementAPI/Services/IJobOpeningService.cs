using HrManagementAPI.DTOs;
using HrManagementAPI.Models;
using HrManagementAPI.Models.RootParameters;

namespace HrManagementAPI.Services
{
    public interface IJobOpeningService
    {
        public Task<List<DtoJobOpening>> GetJobOpeningsAsync(JobOpeningParameters parameters);

        public Task<DtoJobOpening> GetJobOpeningByIdAsync(int openingId);

        public Task<DtoJobOpening> AddJobOpeningAsync(DtoJobOpeningCreate jobOpeningInfo);

        public Task<DtoJobOpening> UpdateJobOpeningAsync(int openingId, DtoJobOpeningCreate jobOpeningInfo);

        public Task DeleteJobOpeningAsync(int openingId);
    }
}
