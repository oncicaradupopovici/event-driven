using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Charisma.Contracts.Data;
using Charisma.Contracts.Domain.EventHandlers;
using Charisma.Contracts.Domain.ReadModel;
using Charisma.Contracts.PublicContracts.Events;
using Charisma.SharedKernel.Data;
using Charisma.SharedKernel.Domain.Interfaces;
using Charisma.SharedKernel.Messaging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Charisma.SharedKernel.EventProcessor;

namespace Charisma.Contracts.EventProcessor
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

            services.AddTransient<IEventSubscriber<ContractCreated>, KafkaConsumer<ContractCreated>>();
            services.AddTransient<IEventSubscriber<ContractAmountUpdated>, KafkaConsumer<ContractAmountUpdated>>();

            services.AddSingleton<TopicRegistry>();

            services.AddTransient<IEventHandler<ContractCreated>, ReadModelGenerator>();
            services.AddTransient<IEventHandler<ContractAmountUpdated>, ReadModelGenerator>();

            services.AddTransient<IReadModelRepository<ContractReadModel>, EfReadModelRepository<ContractReadModel, CharismaContractsDbContext>>();

            services.AddEntityFrameworkSqlServer().AddDbContext<CharismaContractsDbContext>((serviceProvider, options) =>
                options
                    .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Charisma.Contracts.Migrations"))
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
