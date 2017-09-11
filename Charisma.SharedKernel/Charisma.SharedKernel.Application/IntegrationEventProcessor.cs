using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Charisma.SharedKernel.Application.Interfaces;
using Charisma.SharedKernel.Messaging.Abstractions;

namespace Charisma.SharedKernel.Application
{
    public class IntegrationEventProcessor
    {
        private readonly IServiceProvider _serviceProvider;

        public IntegrationEventProcessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task ProcessEventAsync<TEvent>()
            where TEvent : IntegrationEvent
        {
            return Task.Run(async () =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var eventBusSubscriber = scope.ServiceProvider.GetService<IEventBusSubscriber>();
                    var mediator = scope.ServiceProvider.GetService<IMediator>();
                    await eventBusSubscriber.SubscribeAsync<TEvent>(mediator.Publish);
                }
            });
        }

    }
}
