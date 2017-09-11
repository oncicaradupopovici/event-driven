using System.Threading.Tasks;
using Charisma.SharedKernel.EventDrivenAbstractions;

namespace Charisma.SharedKernel.Application.Interfaces
{
    public interface IMediator
    {
        Task Send<TCommand>(TCommand command) where TCommand : Command;
        Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
