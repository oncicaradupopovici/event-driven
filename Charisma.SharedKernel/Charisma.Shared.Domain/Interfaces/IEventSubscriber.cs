using System;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.Domain.Interfaces
{

    public interface IEventSubscriber
    {
        Task SubscribeAsync<TEvent>(Func<TEvent, Task> handler) where TEvent : Event;
    }
}
