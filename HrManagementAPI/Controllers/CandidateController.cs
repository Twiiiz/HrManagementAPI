using HrManagementAPI.Models;
using HrManagementAPI.DTOs;
using HrManagementAPI.QueryParameters;
using HrManagementAPI.Repositories;
using HrManagementAPI.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Npgsql;

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
        [Route("")]
        public async Task<IActionResult> CreateCandidate([FromBody] DtoCreateCandidate candidateInfo)
        {
            var newCandidate = await _candidateService.AddCandidate(candidateInfo);

            return CreatedAtAction(nameof(newCandidate.CandidateId), newCandidate);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> ReplaceCandidate([FromRoute(Name = "id")] int candidateId, [FromBody] DtoCreateCandidate replacement)
        {
            var updCandidate = await _candidateService.UpdateCandidate(candidateId, replacement);

            return Ok(updCandidate);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCandidate([FromRoute(Name = "id")] int candidateId)
        {
            await _candidateService.DeleteCandidate(candidateId);

            return Ok("Candidate was deleted");
        }
    }
}
