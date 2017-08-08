using System.Threading.Tasks;

namespace Charisma.SharedKernel.Core.Interfaces
{
    public interface IMediator
    {
        Task Send<TCommand>(TCommand command) where TCommand : Command;
        Task Publish<TEvent>(TEvent @event) where TEvent : Event;
    }
}
