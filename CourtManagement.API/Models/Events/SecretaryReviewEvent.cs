using CourtManagement.API.Models.Responses;

namespace CourtManagement.API.Models.Events
{
    public class SecretaryReviewEvent
    {
        public int CaseId { get; set; }
        public string Title { get; set; }
        public DateTime AssignedAt { get; set; }

        public SecretaryReviewEvent(CaseResponse @case)
        {
            CaseId = @case.Id;
            Title = @case.Title;
            AssignedAt = DateTime.UtcNow;
        }
    }
}
