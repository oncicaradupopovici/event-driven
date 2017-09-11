using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.EventDrivenAbstractions
{
    public interface IEventHandler<in TEvent>
        where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
