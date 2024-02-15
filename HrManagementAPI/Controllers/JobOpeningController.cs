using HrManagementAPI.Models;
using HrManagementAPI.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HrManagementAPI.Controllers
{
    [Route("api/job_openings")]
    [ApiController]
    public class JobOpeningController : ControllerBase
    {
        private readonly HrManagementContext _context;

        public JobOpeningController(HrManagementContext context) => _context = context;

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetJobOpenings([FromQuery] string? status)
        {
            var jobOpenings = _context.JobOpenings
                        .Include(c => c.Position)
                        .Include(c => c.Office)
                        .AsQueryable();
            var output = jobOpenings;

            if (!string.IsNullOrEmpty(status))
            {
                status = status.ToLower();
                
                switch (status)
                {
                    case "available":
                        output = jobOpenings.Where(x => x.Status == OpeningStatus.available);
                        if (!output.Any())
                            return NoContent();
                        break;

                    case "closed":
                        output = jobOpenings.Where(x => x.Status == OpeningStatus.closed);
                        if (!output.Any())
                            return NoContent();

                        break;

                    case "offer_under_consideration":
                        output = jobOpenings.Where(x => x.Status == OpeningStatus.offer_under_consideration);
                        if (!output.Any())
                            return NoContent();

                        break;
                }
            }

            return Ok(await output.ToListAsync());
        }

        ///////////////
        [HttpGet]
        [Route("{opening_id}")]
        public async Task<IActionResult> GetJobOpening([FromRoute] int hr_id, [FromRoute] int opening_id)
        {
            var jobOpenings = _context.JobOpenings
                .Include(c => c.Position)
                .Include(c => c.Office)
                .Where(x => x.OpeningId == opening_id).AsQueryable();
            if (!jobOpenings.Any())
            {
                return NotFound("Requested job opening was not found");
            }

            var output = await jobOpenings.FirstOrDefaultAsync();

            return Ok(output);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateJobOpening([FromBody] JobOpening new_opening)
        {
            _context.JobOpenings.Add(new_opening);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateJobOpening), new_opening);
        }
    }
}
