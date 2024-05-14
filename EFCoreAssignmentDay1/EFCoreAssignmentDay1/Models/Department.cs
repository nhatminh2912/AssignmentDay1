using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreAssignmentDay1.Models
{
    [Table("Departments")]
    public class Department
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public ICollection<Employee>? Employees { get; set; }
    }
}