using EFCoreAssignmentDay1.DBcontext;
using EFCoreAssignmentDay1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreAssignmentDay1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectEmployeeController : ControllerBase
    {
        private readonly MyContext _context;

        public ProjectEmployeeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectEmployee>>> GetProject_Employees()
        {
            return await _context.ProjectEmployees.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectEmployee>> GetProject_Employee(Guid id)
        {
            var project_Employee = await _context.ProjectEmployees.FindAsync(id);

            if (project_Employee == null)
            {
                return NotFound();
            }
            return project_Employee;
        }

        [HttpPost]
        public async Task<ActionResult<ProjectEmployee>> PostProject_Employee(ProjectEmployee project_Employee)
        {
            _context.ProjectEmployees.Add(project_Employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject_Employee", new { id = project_Employee.Id }, project_Employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject_Employee(Guid id, ProjectEmployee project_Employee)
        {
            if (id != project_Employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(project_Employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Project_EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject_Employee(Guid id)
        {
            var project_Employee = await _context.ProjectEmployees.FindAsync(id);
            if (project_Employee == null)
            {
                return NotFound();
            }

            _context.ProjectEmployees.Remove(project_Employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Project_EmployeeExists(Guid id)
        {
            return _context.ProjectEmployees.Any(e => e.Id == id);
        }
    }
}
