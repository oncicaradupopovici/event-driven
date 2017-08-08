using System.Threading.Tasks;

namespace Charisma.SharedKernel.Core.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(T @event) where T : Event;
    }
}
