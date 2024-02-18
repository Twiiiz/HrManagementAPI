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
            try
            {
                var candidates = await _candidateService.GetCandidatesAsync(candidate_parameters);

                return Ok(candidates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCandidate([FromRoute(Name = "id")] int candidate_id)
        {
            try
            {
                var candidate = await _candidateService.GetCandidateByIdAsync(candidate_id);
                if (candidate == null)
                    return BadRequest("Requested candidate doesn't exist");

                return Ok(candidate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateCandidate([FromBody] DtoCreateCandidate candidateInfo)
        {
            try
            {
                var newCandidate = await _candidateService.AddCandidate(candidateInfo);

                return CreatedAtAction(nameof(newCandidate.CandidateId), newCandidate);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException is PostgresException pgExEmail && pgExEmail.ConstraintName == "contain_at_sign")
                        return BadRequest("Email is invalid. Please ensure it contains '@' symbol");
                    else if (ex.InnerException is PostgresException pgExPhone && pgExPhone.ConstraintName == "no_letters")
                        return BadRequest("Phone number is invalid. Please ensure it doesn't contain any letters");

                    return BadRequest(ex.InnerException);
                }
                else
                    return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> ReplaceCandidate([FromRoute(Name = "id")] int candidateId, [FromBody] DtoCreateCandidate replacement)
        {
            try
            {
                var updCandidate = await _candidateService.UpdateCandidate(candidateId, replacement);

                return Ok(updCandidate);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException is PostgresException pgExEmail && pgExEmail.ConstraintName == "contain_at_sign")
                        return BadRequest("Email is invalid. Please ensure it contains '@' symbol");
                    else if (ex.InnerException is PostgresException pgExPhone && pgExPhone.ConstraintName == "no_letters")
                        return BadRequest("Phone number is invalid. Please ensure it doesn't contain any letters");

                    return BadRequest(ex.InnerException);
                }
                else
                    return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCandidate([FromRoute(Name = "id")] int candidateId)
        {
            try
            {
                await _candidateService.DeleteCandidate(candidateId);

                return Ok("Candidate was deleted");
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                    return StatusCode(500, "Candidate doesn't exist");
                else
                    return StatusCode(500, ex.Message);
            }
        }
    }
}
