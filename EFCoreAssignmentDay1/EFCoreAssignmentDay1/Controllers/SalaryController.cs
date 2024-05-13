using EFCoreAssignmentDay1.DBcontext;
using EFCoreAssignmentDay1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreAssignmentDay1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryController : ControllerBase
    {
        private readonly MyContext _context;

        public SalaryController(MyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salary>>> GetSalaries()
        {
            return await _context.Salaries.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Salary>> GetSalary(Guid id)
        {
            var salary = await _context.Salaries.FindAsync(id);

            if (salary == null)
            {
                return NotFound();
            }

            return salary;
        }

        [HttpPost]
        public async Task<ActionResult<Salary>> PostSalary(Salary salary)
        {
            _context.Salaries.Add(salary);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalary", new { id = salary.Id }, salary);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalary(Guid id, Salary salary)
        {
            if (id != salary.Id)
            {
                return BadRequest();
            }

            _context.Entry(salary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaryExists(id))
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
        public async Task<IActionResult> DeleteSalary(Guid id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            if (salary == null)
            {
                return NotFound();
            }

            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalaryExists(Guid id)
        {
            return _context.Salaries.Any(e => e.Id == id);
        }
    }
}
