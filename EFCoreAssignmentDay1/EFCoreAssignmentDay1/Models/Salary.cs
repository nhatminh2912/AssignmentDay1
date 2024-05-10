using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreAssignmentDay1.Models
{
    [Table("Salaries")]
    public class Salary
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}