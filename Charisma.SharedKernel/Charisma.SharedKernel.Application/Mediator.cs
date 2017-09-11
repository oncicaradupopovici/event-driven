using System;
using System.Threading.Tasks;
using Charisma.SharedKernel.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Charisma.SharedKernel.EventDrivenAbstractions;

namespace Charisma.SharedKernel.Application
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task Publish<TEvent>(TEvent @event)
            where TEvent : IEvent
        {
            var eventHandlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();
            foreach (var eventHandler in eventHandlers)
            {
                await eventHandler.HandleAsync(@event);
            }
        }

        public async Task Send<TCommand>(TCommand command)
            where TCommand : Command
        {
            var commandHandler = _serviceProvider.GetService<ICommandHandler<TCommand>>();
            if (commandHandler != null)
                await commandHandler.HandleAsync(command);
            else
                throw new Exception("Command handler not found");
        }
    }
    
}
