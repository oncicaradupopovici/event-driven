using Charisma.Contracts.Application.CommandHandlers;
using Charisma.Contracts.Application.Commands;
using Charisma.Contracts.Application.EventHandlers;
using Charisma.Contracts.Data.Extensions;
using Charisma.Contracts.PublishedLanguage.Events;
using Charisma.SharedKernel.Application;
using Charisma.SharedKernel.Application.Interfaces;
using Charisma.SharedKernel.Core.Interfaces;
using Charisma.SharedKernel.Messaging.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Charisma.Contracts.Application.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IMediator, Mediator>();

            services.AddScoped<ICommandHandler<CreateContract>, ContractCommandHandlers>();
            services.AddScoped<ICommandHandler<UpdateContractAmount>, ContractCommandHandlers>();

            services.AddScoped<IEventHandler<ContractCreated>, ReadModelGenerator>();
            services.AddScoped<IEventHandler<ContractAmountUpdated>, ReadModelGenerator>();

            services.AddMessaging();
            services.AddDataAccess();
        }
    }
}
