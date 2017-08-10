using Charisma.SharedKernel.Application.Interfaces;
using Charisma.SharedKernel.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Charisma.SharedKernel.Messaging.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddMessaging(this IServiceCollection services)
        {
            services.AddScoped<IEventPublisher, KafkaProducer>();
            services.AddScoped<ICommandSender, KafkaProducer>();
            services.AddScoped<IEventSubscriber, KafkaConsumer>();
            services.AddSingleton<TopicRegistry>();
        }
    }
}
