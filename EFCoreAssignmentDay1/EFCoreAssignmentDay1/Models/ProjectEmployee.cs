using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreAssignmentDay1.Models
{
    [Table("Project_Employee")]
    public class ProjectEmployee
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public bool Enable { get; set; }
    }
}