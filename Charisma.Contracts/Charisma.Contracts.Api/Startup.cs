using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Charisma.Contracts.Domain.Aggregates;
using Charisma.Contracts.Domain.CommandHandlers;
using Charisma.Contracts.Domain.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Charisma.Contracts.Data;
using Charisma.Contracts.Domain.ReadModel;
using Charisma.SharedKernel.Data;
using Microsoft.EntityFrameworkCore;
using Charisma.SharedKernel.Domain.Interfaces;
using Charisma.SharedKernel.Domain;
using Charisma.SharedKernel.Messaging;

namespace Charisma.Contracts.Api
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
            services.AddScoped<ICommandHandler<CreateContract>, ContractCommandHandlers>();
            services.AddScoped<ICommandHandler<UpdateContractAmount>, ContractCommandHandlers>();

            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<IEventRepository, EfEventRepository<CharismaContractsDbContext>>();
            services.AddScoped<IEventSourcedRepository<Contract>, EventSourcedRepository<Contract>>();
            services.AddScoped<IReadModelRepository<ContractReadModel>, EfReadModelRepository<ContractReadModel, CharismaContractsDbContext>>();

            services.AddScoped<IEventPublisher, KafkaProducer>();
            services.AddScoped<ICommandSender, KafkaProducer>();

            services.AddEntityFrameworkSqlServer().AddDbContext<CharismaContractsDbContext>((serviceProvider, options) =>
                options
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Charisma.Contracts.Migrations"))
                    .UseInternalServiceProvider(serviceProvider));

            services.AddSingleton<TopicRegistry>();



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
