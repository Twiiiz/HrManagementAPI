using HrManagementAPI.DTOs;
using HrManagementAPI.Models;
using HrManagementAPI.Models.RootParameters;
using HrManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HrManagementAPI.Controllers
{
    [Route("api/job_openings")]
    [ApiController]
    public class JobOpeningController : ControllerBase
    {
        private readonly IJobOpeningService _jobOpeningService;

        public JobOpeningController(IJobOpeningService jobOpeningService)
        {
            _jobOpeningService = jobOpeningService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetJobOpenings([FromQuery] JobOpeningParameters parameters)
        {
            var jobOpenings = await _jobOpeningService.GetJobOpeningsAsync(parameters);

            return Ok(jobOpenings);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetJobOpening([FromRoute(Name = "id")] int openingId)
        {
            var jobOpening = await _jobOpeningService.GetJobOpeningByIdAsync(openingId);

            return Ok(jobOpening);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateJobOpening([FromBody] DtoJobOpeningCreate jobOpeningInfo)
        {
            var jobOpening = await _jobOpeningService.AddJobOpeningAsync(jobOpeningInfo);

            return CreatedAtAction(nameof(GetJobOpening), new { id = jobOpening.OpeningId }, jobOpening);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> ReplaceJobOpening([FromRoute(Name = "id")] int jobOpeningId, [FromBody] DtoJobOpeningCreate jobOpeningInfo)
        {
            var jobOpening = await _jobOpeningService.UpdateJobOpeningAsync(jobOpeningId, jobOpeningInfo);

            return Ok(jobOpening);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteJobOpening([FromRoute(Name = "id")] int jobOpeningId)
        {
            await _jobOpeningService.DeleteJobOpeningAsync(jobOpeningId);

            return Ok("Job opening was deleted");
        }
    }
}
