using HrManagementAPI.Models;
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
        private readonly HrManagementContext _context;

        public TagController(HrManagementContext context) => _context = context;

        [HttpGet]
        [Route("{hr_id}")]
        public async Task<IActionResult> GetTags([FromRoute] int hr_id)
        {
            var tags = await _context.Tags
                .Include(t => t.TagSubmissions)
                .Where(x => x.HrId == hr_id)
                .AsQueryable()
                .ToListAsync();

            if (!tags.Any())
                return NoContent();

            return Ok(tags);
        }

        [HttpGet]
        [Route("{hr_id}/{tag_id}")]
        public async Task<IActionResult> GetTag([FromRoute] int hr_id, [FromRoute] int tag_id)
        {
            var tags = _context.Tags
                .Include(t => t.TagSubmissions)
                .Where(x => x.HrId == hr_id).AsQueryable();
            if (!tags.Any())
                return NotFound("Provided Hr Manager doesn't have any tags created");

            var tag = await tags
                .Where(x => x.TagId == tag_id)
                .FirstOrDefaultAsync();
            if (tag == null)
            {
                return NotFound("Requested tag for provided Hr Manager was not found");
            }

            return Ok(tag);
        }

        [HttpGet]
        [Route("{hr_id}/{tag_id}/submissions")]
        public async Task<IActionResult> GetTagSubmissions([FromRoute] int hr_id, [FromRoute] int tag_id)
        {
            var action_result = await GetTag(hr_id, tag_id);

            if (action_result is OkObjectResult ok_object_result) // when if - success => ok_object_result = action_result
            {
                var tag = ok_object_result.Value as Tag;

                var tag_submissions = await _context.TagSubmissions
                    .Include(s => s.Tag)
                    .Include(s => s.Sub)
                    .Where(x => x.TagId == tag.TagId)
                    .ToListAsync();

                return Ok(tag_submissions);
            }
            else return NotFound("Unable to find requested tag, or it's not assigned to provided hr manager");
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateTag([FromBody] Tag new_tag)
        {
            new_tag.TagId = default;
            new_tag.CreationDate = DateOnly.FromDateTime(DateTime.Now);
            new_tag.LastUpdateDate = DateOnly.FromDateTime(DateTime.Now);

            _context.Tags.Add(new_tag);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateTag), new_tag);
        }
    }
}
