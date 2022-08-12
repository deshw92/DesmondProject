namespace WebAPIProject.Models
{
    public class Employee : BaseModel
    {
        public string? EmployeeName { get; set; }
        public string? PhotoFileName { get; set; }
        public Department? Department { get; set; }
    }
}
