using HrManagementAPI.Models;
using HrManagementAPI.ModelsMainInfo;
using HrManagementAPI.QueryParameters;
using HrManagementAPI.Repositories;
using HrManagementAPI.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HrManagementAPI.Controllers
{
    [Route("api/candidates")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService) => _candidateService = candidateService;

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCandidates([FromQuery] CandidateParameters candidate_parameters)
        {
            var candidates = await _candidateService.GetCandidatesAsync(candidate_parameters);
            if (!candidates.Any())
                return NotFound();

            return Ok(candidates);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCandidate([FromRoute(Name = "id")] int candidate_id)
        {
            var candidate = await _candidateService.GetCandidateByIdAsync(candidate_id);
            if (candidate == null)
                return BadRequest("Requested candidate doesn't exist");

            return Ok(candidate);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateCandidate([FromBody] CandidateMainInfo candidate_info)
        {
            try
            {
                var new_candidate = await _candidateService.AddCandidate(candidate_info);

                return Ok(new_candidate);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("edit/{candidate-id}")]
        public async Task<IActionResult> ReplaceCandidate([FromRoute(Name = "candidate-id")] int candidate_id, [FromBody] CandidateMainInfo replacement)
        {
            try
            {
                var upd_candidate = await _candidateService.UpdateCandidate(candidate_id, replacement);

                return Ok(upd_candidate);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{candidate_id}")]
        public async Task<IActionResult> DeleteCandidate([FromRoute] int candidate_id)
        {
            var result = await _candidateService.DeleteCandidate(candidate_id);
            if (!result)
                return BadRequest("Candidate doesn't exist");
            else
                return Ok("Candidate was successfully deleted");
        }
    }
}
