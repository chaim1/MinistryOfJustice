using CourtManagement.API.Services.Implementations;
using CourtManagement.API.Services.Interfaces;

namespace CourtManagement.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICaseService, CaseService>();
            services.AddSingleton<IMessageBusService, RabbitMQService>();
            services.AddScoped<INotificationService, NotificationService>();

            return services;
        }
    }
}
