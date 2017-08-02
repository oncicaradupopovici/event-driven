using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.Domain.Interfaces
{
    public interface IEventHandler<in TEvent>
        where TEvent : Event
    {
        Task HandleAsync(TEvent @event);
    }
}
