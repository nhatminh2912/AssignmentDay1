namespace EFCoreAssignmentDay1.DTO
{
    public class EmployeeWithProjectsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid DepartmentId { get; set; }
        public DateTime JoinedDate { get; set; }
        public List<ProjectDto> Projects { get; set; }
    }
}
