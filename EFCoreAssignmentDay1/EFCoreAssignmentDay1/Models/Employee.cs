using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreAssignmentDay1.Models
{
    [Table("Employees")]
    public class Employee
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
        public DateTime JoinedDate { get; set; }
        public Salary Salary { get; set; }
        public ICollection<ProjectEmployee> ProjectEmployees { get; set; }
    }
}