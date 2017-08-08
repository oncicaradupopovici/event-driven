using System;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.Core.Interfaces
{

    public interface IEventSubscriber
    {
        Task SubscribeAsync<TEvent>(Func<TEvent, Task> handler) where TEvent : Event;
    }
}
