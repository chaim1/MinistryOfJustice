using CourtManagement.API.Models.Responses;

namespace CourtManagement.API.Models.Events
{
    public class CaseCreatedEvent
    {
        public int CaseId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }

        public CaseCreatedEvent(CaseResponse @case)
        {
            CaseId = @case.Id;
            Title = @case.Title;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
