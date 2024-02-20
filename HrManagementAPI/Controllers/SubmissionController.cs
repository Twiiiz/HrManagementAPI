using HrManagementAPI.DTOs;
using HrManagementAPI.Models;
using HrManagementAPI.Models.RootParameters;
using HrManagementAPI.Repositories;
using HrManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HrManagementAPI.Controllers
{
    [Route("api/submissions")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly ISubmissionService _submissionService;

        public SubmissionController(ISubmissionService submissionService) => _submissionService = submissionService;

        [HttpGet]
        [Route("hr/{id}")]
        public async Task<IActionResult> GetSubmissions([FromRoute(Name = "id")] int hrId, [FromQuery] SubmissionParameters submissionParameters)
        {
            var submissions = await _submissionService.GetSubmissionsAsync(hrId, submissionParameters);

            return Ok(submissions);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSubmission([FromRoute(Name = "id")] int subId)
        {
            var submission = await _submissionService.GetSubmissionByIdAsync(subId);
            if (submission == null)
                return NotFound("Requested submission doesn't exist");

            return Ok(submission);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateSubmission([FromBody] DtoSubmissionCreate submissionInfo)
        {
            var newSubmission = await _submissionService.AddSubmissionAsync(submissionInfo);

            return CreatedAtAction(nameof(GetSubmission), new { id = newSubmission.SubId }, newSubmission);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> ReplaceSubmission([FromRoute(Name = "id")] int subId, DtoSubmissionCreate submissionInfo)
        {
            var updSubmission = await _submissionService.UpdateSubmissionAsync(subId, submissionInfo);

            return Ok(updSubmission);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteSubmission([FromRoute(Name = "id")] int subId, [FromQuery(Name = "hr-id")][Required] int hrId)
        {
            await _submissionService.DeleteSubmissionAsync(subId, hrId);

            return Ok("Submission was deleted");
        }
    }
}
