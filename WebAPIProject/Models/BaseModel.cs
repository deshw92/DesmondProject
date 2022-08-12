namespace WebAPIProject.Models
{
    public class BaseModel
    {
        public int? Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public bool? Deleted { get;set; }
    }
}
