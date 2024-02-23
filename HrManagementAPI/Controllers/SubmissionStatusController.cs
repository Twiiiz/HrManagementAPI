using HrManagementAPI.DTOs;
using HrManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HrManagementAPI.Controllers
{
    [Route("api/submissions/{sub-id}/statuses")]
    [ApiController]
    public class SubmissionStatusController : ControllerBase
    {
        private readonly ISubmissionStatusService _submissionStatusService;
        
        public SubmissionStatusController(ISubmissionStatusService submissionStatusService)
        {
            _submissionStatusService = submissionStatusService;
        }

        [HttpGet]
        [Route("hr/{hr-id}")]
        public async Task<IActionResult> GetSubmissionStatuses([FromRoute(Name = "sub-id")] int subId, [FromRoute(Name = "hr-id")] int hrId)
        {
            var submissionStatuses = await _submissionStatusService.GetSubmissionStatusesAsync(subId, hrId);

            return Ok(submissionStatuses);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSubmissionStatus([FromRoute(Name = "sub-id")] int subId, [FromRoute(Name = "id")] int subStatId)
        {
            var submissionStatus = await _submissionStatusService.GetSubmissionStatusByIdAsync(subId, subStatId);

            return Ok(submissionStatus);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateSubmissionStatus([FromRoute(Name = "sub-id")] int subId, [FromBody] DtoSubmissionStatusCreate submissionStatusInfo)
        {
            var submissionStatus = await _submissionStatusService.AddSubmissionStatusAsync(subId, submissionStatusInfo);

            return CreatedAtAction(nameof(GetSubmissionStatus), new { id = submissionStatus.SubStatId }, submissionStatus);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> ReplaceSubmissionStatus([FromRoute(Name = "sub-id")] int subId,
            [FromRoute(Name = "id")] int subStatId, [FromBody] DtoSubmissionStatusCreate submissionStatusInfo)
        {
            var submissionStatus = await _submissionStatusService.UpdateSubmissionStatusAsync(subId, subStatId, submissionStatusInfo);

            return Ok(submissionStatus);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteSubmissionStatus([FromRoute(Name = "sub-id")] int subId,
            [FromRoute(Name = "id")] int subStatId, [FromQuery][Required] int hrId)
        {
            await _submissionStatusService.DeleteSubmissionStatusAsync(subId, subStatId, hrId);

            return Ok("Status of provided submission was deleted");
        }
    }
}
