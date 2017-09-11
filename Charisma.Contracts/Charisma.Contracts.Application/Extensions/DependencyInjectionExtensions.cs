using Charisma.Contracts.Application.CommandHandlers;
using Charisma.Contracts.Application.Commands;
using Charisma.Contracts.Application.DomainEventHandlers;
using Charisma.Contracts.Application.IntegrationEventHandlers;
using Charisma.Contracts.Data.Extensions;
using Charisma.SharedKernel.Application;
using Charisma.SharedKernel.Application.Extensions;
using Charisma.SharedKernel.Application.Interfaces;
using Charisma.SharedKernel.EventDrivenAbstractions;
using Charisma.SharedKernel.Messaging.Kafka.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Charisma.Contracts.Application.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddContractsApplication(this IServiceCollection services)
        {
            services.AddMediator();

            services.AddScoped<ICommandHandler<CreateContract>, ContractCommandHandlers>();
            services.AddScoped<ICommandHandler<AddContractLine>, ContractCommandHandlers>();
            services.AddScoped<ICommandHandler<ValidateContract>, ContractCommandHandlers>();

            services.AddScoped<IEventHandler<PublishedLanguage.Events.ContractCreated>, ReadModelGenerator>();
            services.AddScoped<IEventHandler<PublishedLanguage.Events.ContractLineAdded>, ReadModelGenerator>();
            services.AddScoped<IEventHandler<PublishedLanguage.Events.ContractAmountUpdated>, ReadModelGenerator>();
            services.AddScoped<IEventHandler<PublishedLanguage.Events.ContractValidated>, ReadModelGenerator>();

            services.AddScoped<IEventHandler<Domain.ContractAggregate.ContractCreated>, ContractDomainEventHandlers>();
            services.AddScoped<IEventHandler<Domain.ContractAggregate.ContractLineAdded>, ContractDomainEventHandlers>();
            services.AddScoped<IEventHandler<Domain.ContractAggregate.ContractAmountUpdated>, ContractDomainEventHandlers>();
            services.AddScoped<IEventHandler<Domain.ContractAggregate.ContractValidated>, ContractDomainEventHandlers>();

            services.AddKafkaMessaging();
            services.AddContractsDataAccess();
        }
    }
}
