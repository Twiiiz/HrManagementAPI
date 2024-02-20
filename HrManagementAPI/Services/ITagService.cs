using HrManagementAPI.DTOs;

namespace HrManagementAPI.Services
{
    public interface ITagService
    {
        public Task<List<DtoTag>> GetTagsAsync(int hrId);

        public Task<DtoTag> GetTagAsync(int tagId);

        public Task<DtoTag> AddTagAsync(DtoTagCreate tagInfo);

        public Task<DtoTag> UpdateTagAsync(int tagId, int hrId, string tagName);

        public Task DeleteTagAsync(int tagId);


        //public Task<List<DtoTagSubmission>> GetTagSubmissionsAsync(int hrId, int tagId);

        public Task<List<DtoTagSubmission>> GetTagSubmissionsAsync(int tagId);

        public Task<DtoTagSubmission> AddTagSubmissionAsync(int hrId, DtoTagSubmissionCreate tagSubmissionInfo);

        //public Task<DtoTagSubmission> UpdateTagSubmissionAsync(int tagSubmissionId, string tagName);

        public Task DeleteTagSubmissionAsync(int hrId, int tagId);
    }
}
