using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Charisma.Contracts.PublicContracts.Events;
using Charisma.Invoices.Data;
using Charisma.Invoices.Domain.Aggregates;
using Charisma.Invoices.Domain.CommandHandlers;
using Charisma.Invoices.Domain.Commands;
using Charisma.SharedKernel.Data;
using Charisma.SharedKernel.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Charisma.SharedKernel.Domain.Interfaces;
using Charisma.SharedKernel.Messaging;
using Microsoft.EntityFrameworkCore;
using Charisma.Invoices.Domain.EventHandlers;
using Charisma.SharedKernel.Core.Interfaces;

namespace Charisma.Invoices.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<ICommandHandler<CreateInvoice>, InvoiceCommandHandlers>();
            services.AddScoped<ICommandHandler<UpdateInvoiceAmount>, InvoiceCommandHandlers>();

            services.AddScoped<ICrudRepository<Invoice>, EfCrudRepository<Invoice, CharismaInvoicesDbContext>>();
            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<IEventRepository, EfEventRepository<CharismaInvoicesDbContext>>();           
            services.AddScoped<IEventPublisher, KafkaProducer>();
            services.AddSingleton<TopicRegistry>();

            services.AddEntityFrameworkSqlServer().AddDbContext<CharismaInvoicesDbContext>((serviceProvider, options) =>
                options
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Charisma.Invoices.Migrations"))
                    .UseInternalServiceProvider(serviceProvider));

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
