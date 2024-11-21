namespace CourtManagement.API.Services.Interfaces
{
    public interface IMessageBusService
    {
        Task PublishAsync<T>(string eventName, T eventData);
    }
}
