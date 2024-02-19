using HrManagementAPI.Models;
using HrManagementAPI.DTOs;
using HrManagementAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Npgsql;
using HrManagementAPI.Models.RootParameters;

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
                return NotFound("Requested candidate doesn't exist");

            return Ok(candidate);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateCandidate([FromBody] DtoCandidateCreate candidateInfo)
        {
            var newCandidate = await _candidateService.AddCandidateAsync(candidateInfo);

            return CreatedAtAction(nameof(GetCandidate), new { id = newCandidate.CandidateId }, newCandidate);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> ReplaceCandidate([FromRoute(Name = "id")] int candidateId, [FromBody] DtoCandidateCreate replacement)
        {
            var updCandidate = await _candidateService.UpdateCandidateAsync(candidateId, replacement);

            return Ok(updCandidate);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCandidate([FromRoute(Name = "id")] int candidateId)
        {
            await _candidateService.DeleteCandidateAsync(candidateId);

            return Ok("Candidate was deleted");
        }
    }
}
