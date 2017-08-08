using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Charisma.SharedKernel.Core;
using Charisma.SharedKernel.Core.Interfaces;

namespace Charisma.SharedKernel.Messaging
{
    public class FakeBus : ICommandSender, IEventPublisher
    {
        private readonly Dictionary<Type, List<Action<Message>>> _routes = new Dictionary<Type, List<Action<Message>>>();

        public void RegisterHandler<T>(Action<T> handler) where T : Message
        {
            List<Action<Message>> handlers;

            if (!_routes.TryGetValue(typeof(T), out handlers))
            {
                handlers = new List<Action<Message>>();
                _routes.Add(typeof(T), handlers);
            }

            handlers.Add((x => handler((T)x)));
        }

        public Task SendAsync<T>(T command) where T : Command
        {
            List<Action<Message>> handlers;

            if (_routes.TryGetValue(typeof(T), out handlers))
            {
                if (handlers.Count != 1) throw new InvalidOperationException("cannot send to more than one handler");
                handlers[0](command);
            }
            else
            {
                throw new InvalidOperationException("no handler registered");
            }

            return Task.CompletedTask;
        }

        public Task PublishAsync<T>(T @event) where T : Event
        {

            if (!_routes.TryGetValue(@event.GetType(), out List<Action<Message>> handlers))
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
