namespace CourtManagement.API.Models
{
    public class CaseFilter
    {
        public string JudgeId { get; set; }
        public int? Status { get; set; }
        public string SortBy { get; set; } = "openDate";
        public string SortDirection { get; set; } = "desc";
    }
}
