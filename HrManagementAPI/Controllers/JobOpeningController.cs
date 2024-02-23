using HrManagementAPI.Models;
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
        [Route("{id}")]
        public async Task<IActionResult> GetJobOpening([FromRoute(Name = "id")] int openingId)
        {
            var jobOpenings = _context.JobOpenings
                .Include(c => c.Position)
                .Include(c => c.Office)
                .Where(x => x.OpeningId == openingId).AsQueryable();
            if (!jobOpenings.Any())
            {
                return NotFound("Requested job opening was not found");
            }

            var output = await jobOpenings.FirstOrDefaultAsync();

            return Ok(output);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateJobOpening([FromBody] JobOpening newOpening)
        {
            _context.JobOpenings.Add(newOpening);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateJobOpening), newOpening);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> ReplaceJobOpening([FromRoute(Name = "id")] int jobOpeningId)
        {
            return Ok(1);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteJobOpening([FromRoute(Name = "id")] int jobOpeningId)
        {
            return Ok(1);
        }
    }
}
