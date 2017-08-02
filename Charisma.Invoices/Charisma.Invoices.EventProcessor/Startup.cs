using System;
using System.Collections.Generic;
using System.IO;
using Charisma.Invoices.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Charisma.SharedKernel.Domain.Interfaces;
using Charisma.Invoices.Domain.Aggregates;
using Charisma.Invoices.Domain.EventHandlers;
using Charisma.SharedKernel.Messaging;
using Charisma.Contracts.PublicContracts.Events;
using Charisma.SharedKernel.Data;
using Charisma.SharedKernel.Domain;
using Charisma.SharedKernel.EventProcessor;
using Microsoft.EntityFrameworkCore;

namespace Charisma.Invoices.EventProcessor
{
    public class Startup
    {
        private static IConfiguration _configuration;

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_configuration);

            services.AddSingleton<IMediator, Mediator>();
            services.AddTransient<IEventSubscriber, KafkaConsumer>();
            services.AddSingleton<TopicRegistry>();

            services.AddTransient<IEventHandler<ContractCreated>, ContractEventHandlers>();
            services.AddTransient<IEventHandler<ContractAmountUpdated>, ContractEventHandlers>();

            services.AddTransient<ICrudRepository<Invoice>, EfCrudRepository<Invoice, CharismaInvoicesDbContext>>();
            services.AddTransient<IEventStore, EventStore>();
            services.AddTransient<IEventRepository, EfEventRepository<CharismaInvoicesDbContext>>();
            services.AddScoped<IEventPublisher, KafkaProducer>();



            services.AddEntityFrameworkSqlServer().AddDbContext<CharismaInvoicesDbContext>((serviceProvider, options) =>
                options
                    .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Charisma.Invoices.Migrations"))
                    .UseInternalServiceProvider(serviceProvider));

            
        }
    }

    public static class Extensions
    {
        public static IEnumerable<object> GetRequiredServices(this IServiceProvider provider, Type serviceType)
        {
            return (IEnumerable<object>)provider.GetRequiredService(typeof(IEnumerable<>).MakeGenericType(serviceType));
        }
    }
}
