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

        public async Task ProcessEventAsync(Type eventType)
        {
            using (_serviceProvider.CreateScope())
            {
                var eventsubscriberType = typeof(IEventSubscriber<>).MakeGenericType(eventType);
                var eventHandlerType = typeof(IEventHandler<>).MakeGenericType(eventType);

                var eventSubscriber = _serviceProvider.GetService(eventsubscriberType);
                var eventHandler = _serviceProvider.GetService(eventHandlerType);

                if (eventHandler!=null && eventSubscriber!=null)
                {
                    Task awaitable = eventSubscriber.AsDynamic().SubscribeAsync(eventHandler);
                    await awaitable;
                }
            }
        }

        public Task ProcessEventsAsync(Type[] eventTypes)
        {
            return Task.WhenAll(eventTypes.Select(ProcessEventAsync));



        }

    }
}
