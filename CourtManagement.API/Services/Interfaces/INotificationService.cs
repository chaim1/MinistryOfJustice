namespace CourtManagement.API.Services.Interfaces
{
    public interface INotificationService
    {
        Task SendEmailAsync(string recipient, string subject, string body);
        Task SendNotificationAsync(string userId, string message);
    }
}
