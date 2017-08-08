using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Charisma.SharedKernel.Core;
using Charisma.SharedKernel.Core.Interfaces;
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

        public Task ProcessEventAsync<TEvent>()
            where TEvent : Event
        {
            return Task.Run(async () =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {

                    var eventSubscriber = scope.ServiceProvider.GetService<IEventSubscriber>();
                    var mediator = scope.ServiceProvider.GetService<IMediator>();
                    await eventSubscriber.SubscribeAsync<TEvent>(mediator.Publish);
                }
            });
        }

    }
}
