using Charisma.Contracts.PublishedLanguage.Events;
using Charisma.Invoices.Application.CommandHandlers;
using Charisma.Invoices.Application.Commands;
using Charisma.Invoices.Application.EventHandlers;
using Charisma.Invoices.Data.Extensions;
using Charisma.Payments.PublishedLanguage.Events;
using Charisma.SharedKernel.Application;
using Charisma.SharedKernel.Application.Interfaces;
using Charisma.SharedKernel.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Charisma.SharedKernel.Messaging.Extensions;

namespace Charisma.Invoices.Application.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();

            services.AddScoped<ICommandHandler<CreateInvoice>, InvoiceCommandHandlers>();

            services.AddScoped<IEventHandler<ContractValidated>, ContractEventHandlers>();
            services.AddScoped<IEventHandler<PaymentReceived>, PaymentEventHandlers>();

            services.AddMessaging();
            services.AddDataAccess();
        }
    }
}
