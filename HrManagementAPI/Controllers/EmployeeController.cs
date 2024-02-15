using HrManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HrManagementAPI.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly HrManagementContext _context;

        public EmployeeController(HrManagementContext context) => _context = context;

        [HttpGet]
        [Route("skills/{emp_id}")]
        public async Task<IActionResult> GetEmployeeSkills([FromRoute] int emp_id)
        {
            var emp_skills = await _context.EmployeeSkills
                .Where(x => x.EmployeeId == emp_id)
                .AsQueryable()
                .ToListAsync();
            if (!emp_skills.Any())
                return NoContent();

            return Ok(emp_skills);
        }

        [HttpGet]
        [Route("skills/{emp_id}/{emp_skill_id}")]
        public async Task<IActionResult> GetEmployeeSkill([FromRoute] int emp_id, [FromRoute] int emp_skill_id)
        {
            var emp_skills = _context.EmployeeSkills
                .Where(x => x.EmployeeId == emp_id)
                .AsQueryable();
            if (!emp_skills.Any())
                return NotFound("Provided employee doesn't have any skills assigned to them");

            var emp_skill = await emp_skills
                .Where(x => x.EmpSkillId == emp_skill_id)
                .FirstOrDefaultAsync();
            if (emp_skill == null)
                return NotFound("Requested skill of provided employee was not found");

            return Ok(emp_skill);
        }
    }
}
