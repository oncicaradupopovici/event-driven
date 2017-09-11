using Charisma.Payments.Application.CommandHandlers;
using Charisma.SharedKernel.Application;
using Charisma.SharedKernel.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Charisma.Invoices.PublishedLanguage.Events;
using Charisma.Payments.Application.Commands;
using Charisma.Payments.Data.Extensions;
using Charisma.SharedKernel.Messaging.Kafka.Extensions;
using Charisma.SharedKernel.EventDrivenAbstractions;
using Charisma.SharedKernel.Application.Extensions;
using Charisma.Payments.Application.IntegrationEventHandlers;
using Charisma.Payments.Application.DomainEventHandlers;

namespace Charisma.Payments.Application.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediator();

            services.AddScoped<ICommandHandler<PayPayable>, PayableCommandHandlers>();

            services.AddScoped<IEventHandler<InvoiceCreated>, InvoiceIntegrationEventHandlers>();

            services.AddScoped<IEventHandler<Domain.PayableAggregate.PaymentReceived>, PaymentDomainEventHandlers>();

            services.AddKafkaMessaging();
            services.AddPaymentsDataAccess();
        }
    }
}
