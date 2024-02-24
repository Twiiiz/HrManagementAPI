using HrManagementAPI.DTOs;
using HrManagementAPI.Mappers;
using HrManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HrManagementAPI.Services
{
    public class TagService: ITagService
    {
        private readonly HrManagementContext _context;
        private readonly Mapper _mapper;

        public TagService(HrManagementContext context)
        {
            _context = context;
            _mapper = new Mapper();
        }

        public async Task<List<DtoTag>> GetTagsAsync(int hrId) 
        {
            var tags = _context.Tags.Where(x => x.HrId ==  hrId).AsQueryable();

            return await tags.Select(x => _mapper.EntityToDto(x)).ToListAsync();
        }

        public async Task<DtoTag> GetTagAsync(int tagId)
        {
            return await _context.Tags
                .Where(x => x.TagId == tagId)
                .Select(x => _mapper.EntityToDto(x))
                .AsQueryable()
                .FirstOrDefaultAsync();
        }

        public async Task<DtoTag> AddTagAsync(DtoTagCreate tagInfo)
        {
            if (!await IsTagUnique(tagInfo))
                throw new ArgumentException("Tag with identical data already exists");

            var tag = _mapper.DtoToEntity(tagInfo);
            tag.CreationDate = DateOnly.FromDateTime(DateTime.Today);

            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();

            return _mapper.EntityToDto(tag);
        }

        public async Task<DtoTag> UpdateTagAsync(int tagId, int hrId, string tagName)
        {
            var tag = await _context.Tags.Where(x => x.TagId == tagId).FirstOrDefaultAsync();
            DtoTagCreate tagInfo = new DtoTagCreate { TagName = tagName, HrId = tag.HrId };
            if (!await IsTagUnique(tagInfo))
                throw new ArgumentException("Tag with identical data already exists");
            if (hrId != tag.HrId)
                throw new ArgumentException("Current HR manager doesn't have access to the tag");

            tag.TagName = tagName;
            tag.CreationDate = tag.CreationDate;
            tag.LastUpdateDate = DateOnly.FromDateTime(DateTime.Today);
            tag.HrId = tag.HrId;

            await _context.SaveChangesAsync();

            return _mapper.EntityToDto(tag);
        }

        public async Task DeleteTagAsync(int tagId)
        {
            _context.Tags.Remove(_context.Tags.First(x => x.TagId == tagId));
            await _context.SaveChangesAsync();
        }

        private async Task<bool> IsTagUnique(DtoTagCreate tagInfo)
        {
            var tag = await _context.Tags.Where(x => x.TagName == tagInfo.TagName &&
                                    x.HrId == tagInfo.HrId).FirstOrDefaultAsync();

            if (tag != null)
                return false;

            return true;
        }

        public async Task<List<DtoTagSubmission>> GetTagSubmissionsAsync(int tagId)
        {
            var tagSubmissions = await _context.TagSubmissions
                .Where(x => x.TagId == tagId)
                .Select(x => _mapper.EntityToDto(x))
                .ToListAsync();

            return tagSubmissions;
        }

        public async Task<DtoTagSubmission> AddTagSubmissionAsync(int hrId, DtoTagSubmissionCreate tagSubmissionInfo)
        {
            var submission = await _context.CandidateSubmissions
                .Where(x => x.SubId == tagSubmissionInfo.SubId && x.HrId != hrId)
                .FirstOrDefaultAsync();
            if (submission != null)
                throw new ArgumentException("Current HR manager doesn't have access to the candidate submission");

            var tag = await _context.Tags.Where(x => x.TagId == tagSubmissionInfo.TagId).FirstOrDefaultAsync();

            if (tag.HrId != hrId)
                throw new ArgumentException("Current HR manager doesn't have access to the tag");
            
            if (!await IsTagSubmissionUnique(tagSubmissionInfo))
                throw new ArgumentException("Current submission is already assigned to current tag");

            tag.LastUpdateDate = DateOnly.FromDateTime(DateTime.Today);
            var tagSubmission = _mapper.DtoToEntity(tagSubmissionInfo);
            _context.TagSubmissions.Add(tagSubmission);
            await _context.SaveChangesAsync();

            return _mapper.EntityToDto(tagSubmission);
        }

        public async Task DeleteTagSubmissionAsync(int hrId, int tagSubmissionId)
        {
            var tagSubmission = _context.TagSubmissions.Where(x => x.SubTagId == tagSubmissionId).First();
            if (!await IsTagConnected(hrId, tagSubmission.TagId))
                throw new ArgumentException("Current HR manager doesn't have access to the tag and its submissions");

            _context.TagSubmissions.Remove(tagSubmission);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> IsTagSubmissionUnique(DtoTagSubmissionCreate tagSubmissionInfo)
        {
            var tagSubmission = await _context.TagSubmissions.Where(x => x.TagId == tagSubmissionInfo.TagId &&
                                                              x.SubId == tagSubmissionInfo.SubId).FirstOrDefaultAsync();
            if (tagSubmission != null)
                return false;

            return true;
        }

        private async Task<bool> IsTagConnected(int hrId, int tagId)
        {
            var tag = await _context.Tags.Where(x => x.TagId == tagId).FirstOrDefaultAsync();

            if (tag.HrId != hrId)
                return false;

            return true;
        }
    }
}
