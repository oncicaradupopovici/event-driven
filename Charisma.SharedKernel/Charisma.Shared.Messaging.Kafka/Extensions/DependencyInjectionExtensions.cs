using Charisma.SharedKernel.Messaging.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Charisma.SharedKernel.Messaging.Kafka.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddKafkaMessaging(this IServiceCollection services)
        {
            services.AddScoped<IEventBusPublisher, KafkaProducer>();
            services.AddScoped<IEventBusSubscriber, KafkaConsumer>();
            services.AddSingleton<TopicRegistry>();
        }
    }
}
