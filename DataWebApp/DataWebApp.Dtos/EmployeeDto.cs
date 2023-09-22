namespace DataWebApp.Dtos
{
    public class EmployeeDto
    {
        public int? EmployeeId { get; set; }
        public string UserId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; } = true;
        public int? ManagerId { get; set; }
    }
}