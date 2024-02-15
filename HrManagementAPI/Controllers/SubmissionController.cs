using HrManagementAPI.Models;
using HrManagementAPI.QueryParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HrManagementAPI.Controllers
{
    [Route("api/submissions")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly HrManagementContext _context;

        public SubmissionController(HrManagementContext context) => _context = context;

        [HttpGet]
        [Route("{hr_id}")]
        public async Task<IActionResult> GetSubmissions([FromRoute] int hr_id, [FromQuery] CandidateSubmissionParameters cand_sub_params)
        {
            var submissions = _context.CandidateSubmissions.Where(x => x.HrId == hr_id).AsQueryable();

            if (submissions.Any())
            {
                if (cand_sub_params.candidate_id != null)
                    submissions = submissions.Where(x => x.CandidateId == cand_sub_params.candidate_id);

                if (!string.IsNullOrEmpty(cand_sub_params.job_position))
                    submissions = submissions.Where(x => x.JobPosition.ToLower().Contains(cand_sub_params.job_position.ToLower()));

                if (!string.IsNullOrEmpty(cand_sub_params.prefferred_location))
                    submissions = submissions.Where(x => x.PrefferredLocation.Any(l => l.Contains(cand_sub_params.prefferred_location, StringComparison.OrdinalIgnoreCase)));
            }

            if (!submissions.Any())
            {
                return NoContent();
            }

            return Ok(await submissions.ToListAsync());
        }

        [HttpGet]
        [Route("{hr_id}/{sub_id}")]
        public async Task<IActionResult> GetSubmission([FromRoute] int hr_id, [FromRoute] int sub_id)
        {
            var hr_submissions = _context.CandidateSubmissions.Where(x => x.HrId == hr_id).AsQueryable();
            if (!hr_submissions.Any())
                return NotFound("Provided Hr Manager doesn't have any submissions assigned to them");

            var submission = await hr_submissions.Where(x => x.SubId == sub_id).FirstOrDefaultAsync();
            if (submission == null)
                return NotFound("Requested submission was not found for provided Hr Manager");

            return Ok(submission);
        }

        [HttpGet]
        [Route("{hr_id}_{sub_id}/statuses")]
        public async Task<IActionResult> GetSubmissionStatuses([FromRoute] int hr_id, [FromRoute] int sub_id, [FromQuery] SubmissionStatusParameters sub_stat_params)
        {
            var hr_submissions = _context.CandidateSubmissions.Where(x => x.HrId == hr_id).AsQueryable();
            if (!hr_submissions.Any())
                return NotFound("Provided Hr Manager doesn't have any submissions assigned to them");

            var submission = hr_submissions.Where(x => x.SubId == sub_id).FirstOrDefault();
            if (submission == null)
                return NotFound("Requested submission was not found for provided Hr Manager");

            var statuses = _context.SubmissionStatuses.Where(x => x.SubId == submission.SubId).AsQueryable();
            
            if (statuses.Any())
            {
                if (sub_stat_params.MoreStatusYear != null)
                {
                    if (sub_stat_params.MoreStatusYear < 0)
                        return BadRequest("Inappropriate value was set for a query parameter");
                    else
                        statuses = statuses.Where(x => x.StatusDate.Year >= sub_stat_params.MoreStatusYear);
                }

                if (sub_stat_params.LessStatusYear != null)
                {
                    if (sub_stat_params.LessStatusYear < 0 | sub_stat_params.LessStatusYear > DateTime.Today.Year)
                        return BadRequest("Inappropriate value was set for a query parameter");
                    else
                        statuses = statuses.Where(x => x.StatusDate.Year <= sub_stat_params.LessStatusYear);
                }
            }

            if (!statuses.Any())
                return NoContent();

            return Ok(await statuses.ToListAsync());
        }

        [HttpGet]
        [Route("{hr_id}_{sub_id}/statuses/{status_id}")]
        public async Task<IActionResult> GetSubmissionStatus([FromRoute] int hr_id, [FromRoute] int sub_id, [FromRoute] int status_id)
        {
            var hr_submissions = _context.CandidateSubmissions.Where(x => x.HrId == hr_id).AsQueryable();
            if (!hr_submissions.Any())
                return NotFound("Provided Hr Manager doesn't have any submissions assigned to them");

            var submission = hr_submissions.Where(x => x.SubId == sub_id).FirstOrDefault();
            if (submission == null)
                return NotFound("Provided submission was not found for provided Hr Manager");

            var statuses = _context.SubmissionStatuses.Where(x => x.SubId == submission.SubId).AsQueryable();
            if (!statuses.Any())
                return NotFound("Provided submission doesn't have any statuses assigned to them");

            var status = await statuses.Where(x => x.SubStatId == status_id).FirstOrDefaultAsync();
            if (status == null)
                return NotFound("Requested status of provided submission was not found");

            return Ok(status);
        }

        [HttpGet]
        [Route("{hr_id}_{sub_id}/skills")]
        public async Task<IActionResult> GetSubmissionSkills([FromRoute] int hr_id, [FromRoute] int sub_id)
        {
            var hr_submissions = _context.CandidateSubmissions.Where(x => x.HrId == hr_id).AsQueryable();
            if (!hr_submissions.Any())
                return NotFound("Provided Hr Manager doesn't have any submissions assigned to them");

            var submission = hr_submissions.Where(x => x.SubId == sub_id).FirstOrDefault();
            if (submission == null)
                return NotFound("Requested submission was not found for provided Hr Manager");

            var sub_skills = _context.SubmissionSkills.Where(x => x.SubId == submission.SubId).AsQueryable();
            if (!sub_skills.Any())
                return NoContent();

            return Ok(await sub_skills.ToListAsync());
        }

        [HttpGet]
        [Route("{hr_id}_{sub_id}/notes")]
        public async Task<IActionResult> GetSubmissionNotes([FromRoute] int hr_id, [FromRoute] int sub_id)
        {
            var hr_submissions = _context.CandidateSubmissions.Where(x => x.HrId == hr_id).AsQueryable();
            if (!hr_submissions.Any())
                return NotFound("Provided Hr Manager doesn't have any submissions assigned to them");

            var submission = hr_submissions.Where(x => x.SubId == sub_id).FirstOrDefault();
            if (submission == null)
                return NotFound("Requested submission was not found for provided Hr Manager");

            var sub_notes = _context.Notes.Where(x => x.SubId == submission.SubId).AsQueryable();
            if (!sub_notes.Any())
                return NoContent();

            return Ok(await sub_notes.ToListAsync());
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateSubmission([FromBody] CandidateSubmission new_submission)
        {
            _context.CandidateSubmissions.Add(new_submission);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateSubmission), new_submission);
        }
    }
}
