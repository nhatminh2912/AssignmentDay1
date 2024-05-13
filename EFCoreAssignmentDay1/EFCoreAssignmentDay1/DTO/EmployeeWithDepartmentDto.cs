namespace EFCoreAssignmentDay1.DTO
{
    public class EmployeeWithDepartmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}
