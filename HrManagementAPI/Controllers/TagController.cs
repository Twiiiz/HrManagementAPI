using HrManagementAPI.DTOs;
using HrManagementAPI.Models;
using HrManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;

namespace HrManagementAPI.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService) => _tagService = tagService;

        [HttpGet]
        [Route("hr/{id}")]
        public async Task<IActionResult> GetTags([FromRoute(Name = "id")] int hrId)
        {
            var tags = await _tagService.GetTagsAsync(hrId);
            if (!tags.Any())
                return NotFound("Current HR manager doesn't have any tags yet");

            return Ok(tags);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTag([FromRoute(Name = "id")] int tagId)
        {
            var tag = await _tagService.GetTagAsync(tagId);
            if (tag == null)
                return NotFound("Requested tag doesn't exist");

            return Ok(tag);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateTag([FromBody] DtoTagCreate tagInfo)
        {
            var newTag = await _tagService.AddTagAsync(tagInfo);

            return CreatedAtAction(nameof(GetTag), new { id = newTag.TagId }, newTag);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> ReplaceTag([FromRoute(Name = "id")] int tagId, [FromBody] string tagName)
        {
            var updTag = await _tagService.UpdateTagAsync(tagId, tagName);

            return Ok(updTag);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTag([FromRoute(Name = "id")] int tagId)
        {
            await _tagService.DeleteTagAsync(tagId);

            return Ok("Tag was deleted");
        }

        [HttpGet]
        [Route("{id}/tag-submissions")]
        public async Task<IActionResult> GetSubmissionsOfTag([FromRoute(Name = "id")] int tagId)
        {
            var tagSubmissions = await _tagService.GetTagSubmissionsAsync(tagId);

            return Ok(tagSubmissions);
        }

        [HttpPost]
        [Route("hr/{id}/tag-submissions")]
        public async Task<IActionResult> CreateTagSubmission([FromRoute(Name = "id")] int hrId, [FromBody] DtoTagSubmissionCreate tagSubmissionInfo)
        {
            var newTagSubmission = await _tagService.AddTagSubmissionAsync(hrId, tagSubmissionInfo);

            return CreatedAtAction(nameof(GetTag), new { id = newTagSubmission.SubTagId }, newTagSubmission);
        }

        [HttpDelete]
        [Route("hr/{hr-id}/tag-submissions/{sub-tag-id}")]
        public async Task<IActionResult> DeleteTagSubmission([FromRoute(Name = "hr-id")] int hrId, [FromRoute(Name = "sub-tag-id")] int tagSubmissionId)
        {
            await _tagService.DeleteTagSubmissionAsync(hrId, tagSubmissionId);

            return Ok("Relation between submission and a tag was deleted");
        }
    }
}
