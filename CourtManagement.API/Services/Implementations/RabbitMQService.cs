using CourtManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;

namespace CourtManagement.API.Services.Implementations
{
    public class RabbitMQService : IMessageBusService
    {
        private readonly ILogger<RabbitMQService> _logger;

        public RabbitMQService(ILogger<RabbitMQService> logger)
        {
            _logger = logger;
        }

        public Task PublishAsync<T>(string eventName, T data)
        {
            _logger.LogInformation($"Event {eventName} published with data: {JsonSerializer.Serialize(data)}");
            return Task.CompletedTask;
        }
    }
}
