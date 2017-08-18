using Charisma.Payments.Application.CommandHandlers;
using Charisma.SharedKernel.Application;
using Charisma.SharedKernel.Application.Interfaces;
using Charisma.SharedKernel.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Charisma.Invoices.PublishedLanguage;
using Charisma.Invoices.PublishedLanguage.Events;
using Charisma.Payments.Application.Commands;
using Charisma.Payments.Application.EventHandlers;
using Charisma.Payments.Data.Extensions;
using Charisma.SharedKernel.Messaging.Extensions;

namespace Charisma.Payments.Application.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();

            services.AddScoped<ICommandHandler<PayPayable>, PayableCommandHandlers>();

            services.AddScoped<IEventHandler<InvoiceCreated>, InvoiceEventHandlers>();

            services.AddMessaging();
            services.AddDataAccess();
        }
    }
}
