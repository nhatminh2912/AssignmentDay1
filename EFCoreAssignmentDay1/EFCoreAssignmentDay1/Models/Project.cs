using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreAssignmentDay1.Models
{
    [Table("Projects")]
    public class Project
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public ICollection<ProjectEmployee>? ProjectEmployees { get; set; }
    }
}