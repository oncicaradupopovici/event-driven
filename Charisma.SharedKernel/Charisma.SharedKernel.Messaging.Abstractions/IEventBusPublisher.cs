using System.Threading.Tasks;

namespace Charisma.SharedKernel.Messaging.Abstractions
{
    public interface IEventBusPublisher
    {
        Task PublishAsync<T>(T @event) where T : IntegrationEvent;
    }
}
