namespace CourtManagement.API.Models.Responses
{
    public class CaseResponse
    {
        public int Id { get; set; }
        public string CaseNumber { get; set; }
        public string Title { get; set; }
        public DateTime OpenDate { get; set; }
        public string StatusDsc { get; set; }
        public int Status { get; set; }
        public string JudgeId { get; set; }
         public string JudgeName { get; set; }
    }
}
