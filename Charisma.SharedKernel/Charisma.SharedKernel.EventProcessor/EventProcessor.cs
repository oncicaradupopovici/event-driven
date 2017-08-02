using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Charisma.SharedKernel.Domain;
using Charisma.SharedKernel.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Charisma.SharedKernel.EventProcessor
{
    public class EventProcessor
    {
        private readonly IServiceProvider _serviceProvider;

        public EventProcessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ProcessEventAsync<TEvent>()
            where TEvent : Event
        {
            await Task.Run(async () =>
            {
                using (_serviceProvider.CreateScope())
                {

                    var eventSubscriber = _serviceProvider.GetService<IEventSubscriber>();
                    var mediator = _serviceProvider.GetService<IMediator>();
                    await eventSubscriber.SubscribeAsync<TEvent>(mediator.Publish);
                }
            });
        }

    }
}
