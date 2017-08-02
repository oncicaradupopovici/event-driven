using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.Domain.Interfaces
{
    public interface IMediator
    {
        Task Send<TCommand>(TCommand command) where TCommand : Command;
        Task Publish<TEvent>(TEvent @event) where TEvent : Event;
    }
}
