using System.Threading.Tasks;

namespace Charisma.SharedKernel.Domain.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(T @event) where T : Event;
    }
}
