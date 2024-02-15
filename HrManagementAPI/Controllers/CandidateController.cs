using HrManagementAPI.Models;
using HrManagementAPI.ModelsMainInfo;
using HrManagementAPI.QueryParameters;
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
        private readonly HrManagementContext _context;

        public CandidateController(HrManagementContext context) => _context = context;

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCandidates([FromQuery] CandidateParameters candidate_parameters)
        {
            var candidates = _context.Candidates
                .Select(x => new CandidateMainInfo
                {
                    CandidateId = x.CandidateId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BirthDate = x.BirthDate,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    Status = x.Status
                })
                .AsQueryable();

            if (candidates.Any())
            {
                if (!string.IsNullOrEmpty(candidate_parameters.first_name))
                    candidates = candidates.Where(x => x.FirstName.ToLower() == candidate_parameters.first_name.ToLower());

                if (!string.IsNullOrEmpty(candidate_parameters.last_name))
                    candidates = candidates.Where(x => x.LastName.ToLower() == candidate_parameters.last_name.ToLower());

                if (!string.IsNullOrEmpty(candidate_parameters.status))
                {
                    var status = candidate_parameters.status.ToLower();

                    switch (status)
                    {
                        case "hired":
                            candidates = candidates.Where(x => x.Status == CandidateStatus.hired);
                            
                            break;

                        case "not_hired":
                            candidates = candidates.Where(x => x.Status == CandidateStatus.not_hired);

                            break;

                        case "offer_made":
                            candidates = candidates.Where(x => x.Status == CandidateStatus.offer_made);

                            break;

                        case "offer_denied":
                            candidates = candidates.Where(x => x.Status == CandidateStatus.offer_denied);

                            break;

                        case "spam":
                            candidates = candidates.Where(x => x.Status == CandidateStatus.spam);

                            break;
                    }
                }

                if (!candidates.Any())
                    return NoContent();

                return Ok(await candidates.ToListAsync());
            }
            else return Problem();
        }

        [HttpGet]
        [Route("{candidate_id}")]
        public async Task<IActionResult> GetCandidate([FromRoute] int candidate_id)
        {
            var candidate = await _context.Candidates
                .Where(x => x.CandidateId == candidate_id)
                .AsQueryable()
                .FirstOrDefaultAsync();
            if (candidate == null)
            {
                return NotFound(new { error_message = "No such candidate was found" });
            }

            return Ok(candidate);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateCandidate([FromBody] Candidate new_candidate)
        {
            new_candidate.CandidateId = default;
            _context.Candidates.Add(new_candidate);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateCandidate), new_candidate);
        }

        [HttpPut]
        [Route("edit/{candidate_id}")]
        public async Task<IActionResult> ReplaceCandidate([FromRoute] int candidate_id, [FromBody] CandidateMainInfo replacement)
        {
            var candidate = _context.Candidates.FirstOrDefault(x => x.CandidateId == candidate_id);
            if (candidate != null)
            {
                candidate.FirstName = replacement.FirstName;
                candidate.LastName = replacement.LastName;
                candidate.Status = replacement.Status;
                candidate.BirthDate = replacement.BirthDate;
                candidate.PhoneNumber = replacement.PhoneNumber;
                candidate.Email = replacement.Email;

                await _context.SaveChangesAsync();

                return Ok(candidate);
            }
            else return NotFound("Requested candidate was not found");
        }

        [HttpDelete]
        [Route("delete/{candidate_id}")]
        public async Task<IActionResult> DeleteCandidate([FromRoute] int candidate_id)
        {
            var candidate = _context.Candidates.FirstOrDefault(x => x.CandidateId == candidate_id);
            if (candidate != null)
            {
                _context.Candidates.Remove(_context.Candidates.First(x => x.CandidateId == candidate_id));
                await _context.SaveChangesAsync();

                return Ok("Requested candidate was deleted");
            }
            else return NotFound("Requested candidate was not found");
        }
    }
}
