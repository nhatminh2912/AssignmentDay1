using EFCoreAssignmentDay1.DBcontext;
using Microsoft.AspNetCore.Mvc;
using EFCoreAssignmentDay1.DTO;
using Microsoft.EntityFrameworkCore;
using EFCoreAssignmentDay1.Models;

namespace EFCoreAssignmentDay1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueryDataController : ControllerBase
    {
        private readonly MyContext _context;
        public QueryDataController(MyContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/employeeswithdepartment")]
        public async Task<ActionResult<IEnumerable<EmployeeWithDepartmentDto>>> GetEmployeesWithDepartment()
        {
            var employeesWithDepartment = await _context.EmployeeWithDepartmentDto.FromSqlRaw(
                "SELECT e.Id, e.Name, e.JoinedDate, d.Name AS DepartmentName " +
                "FROM Employees e " +
                "INNER JOIN Departments d ON e.DepartmentId = d.Id"
            ).ToListAsync();

            return employeesWithDepartment;
        }

        [HttpGet]
        [Route("api/employeeswithprojects")]
        public async Task<ActionResult<IEnumerable<EmployeeWithProjectsDto>>> GetEmployeesWithProjects()
        {
            var employeesWithProjects = await _context.EmployeeWithProjectsDto.FromSqlRaw(
                "SELECT e.Id, e.Name, e.DepartmentId, e.JoinedDate, " +
                "       p.Id AS ProjectId, p.Name AS ProjectName " +
                "FROM Employees e " +
                "LEFT JOIN Project_Employees pe ON e.Id = pe.EmployeeId " +
                "LEFT JOIN Projects p ON pe.ProjectId = p.Id"
            ).ToListAsync();

            // Grouping employees with their projects
            var groupedEmployeesWithProjects = employeesWithProjects
                .GroupBy(e => new { e.Id, e.Name, e.DepartmentId, e.JoinedDate })
                .Select(g => new EmployeeWithProjectsDto
                {
                    Id = g.Key.Id,                    Name = g.Key.Name,
                    DepartmentId = g.Key.DepartmentId,
                    JoinedDate = g.Key.JoinedDate,
                    Projects = g.Select(p => new ProjectDto { Id = p.Id, Name = p.Name }).ToList()
                });

            return groupedEmployeesWithProjects.ToList();
        }

        [HttpGet]
        [Route("api/employeeswithcriteria")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesWithCriteria()
        {
            var employeesWithCriteria = await _context.Employees.FromSqlRaw(
                "SELECT * " +
                "FROM Employees " +
                "WHERE Salary > 100 AND JoinedDate >= '2024-01-01'"
            ).ToListAsync();

            return employeesWithCriteria;
        }
    }
}
