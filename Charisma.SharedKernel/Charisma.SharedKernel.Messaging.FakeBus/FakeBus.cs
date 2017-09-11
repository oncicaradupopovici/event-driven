using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Charisma.SharedKernel.Messaging.Abstractions;

namespace Charisma.SharedKernel.Messaging
{
    public class FakeBus : IEventBusPublisher
    {
        private readonly Dictionary<Type, List<Action<IntegrationEvent>>> _routes = new Dictionary<Type, List<Action<IntegrationEvent>>>();

        public void RegisterHandler<T>(Action<T> handler) where T : IntegrationEvent
        {
            List<Action<IntegrationEvent>> handlers;

            if (!_routes.TryGetValue(typeof(T), out handlers))
            {
                handlers = new List<Action<IntegrationEvent>>();
                _routes.Add(typeof(T), handlers);
            }

            handlers.Add((x => handler((T)x)));
        }


        public Task PublishAsync<T>(T @event) where T : IntegrationEvent
        {

            if (!_routes.TryGetValue(@event.GetType(), out List<Action<IntegrationEvent>> handlers))
                return Task.CompletedTask;

            foreach (var handler in handlers)
            {
                //dispatch on thread pool for added awesomeness
                var handler1 = handler;
                Task.Run(() => { handler1(@event); });
            }

            return Task.CompletedTask;

        }
    }
}
