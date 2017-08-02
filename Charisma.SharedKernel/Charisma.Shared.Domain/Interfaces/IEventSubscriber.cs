using System.Threading.Tasks;

namespace Charisma.SharedKernel.Domain.Interfaces
{

    public interface IEventSubscriber<out TEvent>
        where TEvent : Event
    {
        Task SubscribeAsync(IEventHandler<TEvent> handler);
    }
}
