using Charisma.SharedKernel.Application.Interfaces;
using Charisma.SharedKernel.EventDrivenAbstractions;
using Charisma.SharedKernel.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Charisma.SharedKernel.Application
{
    public class MediatorEventPublisher : IEventPublisher
    {
        private readonly IMediator _mediator;

        public MediatorEventPublisher(IMediator mediator)
        {
            _mediator = mediator;
        }

        //public Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        //{
        //    return _mediator.Publish(@event);
        //}

        public Task PublishAsync(IEvent @event)
        {
            var eventType = @event.GetType();
            //return _mediator.AsDynamic().Publish(@event) as Task;
            //return _mediator.Publish(@event);
            var task = ReflectionUtils.CallGenericMethod(_mediator, "Publish", eventType, @event) as Task;
            return task;
        }
    }
}
