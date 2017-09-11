using Charisma.SharedKernel.Application.Interfaces;
using Charisma.SharedKernel.EventDrivenAbstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Charisma.SharedKernel.Application.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddMediator(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddScoped<IEventPublisher, MediatorEventPublisher>();
        }
    }
}
