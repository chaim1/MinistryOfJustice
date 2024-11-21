using CourtManagement.API.Services.Interfaces;

namespace CourtManagement.API.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string recipient, string subject, string body)
        {
            _logger.LogInformation("Sending email to {Recipient}: {Subject}", recipient, subject);
            return Task.CompletedTask;
        }

        public Task SendNotificationAsync(string userId, string message)
        {
            _logger.LogInformation("Sending notification to {UserId}: {Message}", userId, message);
            return Task.CompletedTask;
        }
    }
}
