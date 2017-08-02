﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Charisma.SharedKernel.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Charisma.SharedKernel.Domain
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task Publish<TEvent>(TEvent @event)
            where TEvent : Event
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