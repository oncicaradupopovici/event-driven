using Charisma.Invoices.Application.CommandHandlers;
using Charisma.Invoices.Application.Commands;
using Charisma.Invoices.Application.IntegrationEventHandlers;
using Charisma.Invoices.Data.Extensions;
using Charisma.SharedKernel.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Charisma.SharedKernel.Messaging.Kafka.Extensions;
using Charisma.SharedKernel.EventDrivenAbstractions;
using Charisma.SharedKernel.Application.Extensions;
using Charisma.Invoices.Application.DomainEventHandlers;

namespace Charisma.Invoices.Application.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediator();

            services.AddScoped<ICommandHandler<CreateInvoice>, InvoiceCommandHandlers>();

            services.AddScoped<IEventHandler<Contracts.PublishedLanguage.Events.ContractValidated>, ContractIntegrationEventHandlers>();
            services.AddScoped<IEventHandler<Payments.PublishedLanguage.Events.PaymentReceived>, PaymentIntegrationEventHandlers>();

            services.AddScoped<IEventHandler<Domain.InvoiceAggregate.InvoiceCreated>, InvoiceDomainEventHandlers>();

            services.AddKafkaMessaging();
            services.AddInvoicesDataAccess();
        }
    }
}
