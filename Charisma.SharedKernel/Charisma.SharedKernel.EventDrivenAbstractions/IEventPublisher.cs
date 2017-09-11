using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.EventDrivenAbstractions
{
    public interface IEventPublisher
    {
        //Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;
        Task PublishAsync(IEvent @event);
    }
}
