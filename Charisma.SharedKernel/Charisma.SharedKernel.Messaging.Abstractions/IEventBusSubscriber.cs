using System;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.Messaging.Abstractions
{
    public interface IEventBusSubscriber
    {
        Task SubscribeAsync<TEvent>(Func<TEvent, Task> handler) where TEvent : IntegrationEvent;
    }
}
