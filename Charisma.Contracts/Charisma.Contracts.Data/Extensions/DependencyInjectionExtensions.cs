using Charisma.Contracts.Domain.ContractAggregate;
using Charisma.Contracts.ReadModel.Entities;
using Charisma.SharedKernel.Data.Abstractions;
using Charisma.SharedKernel.Data.EntityFramework;
using Charisma.SharedKernel.Data.EventSourcing;
using Charisma.SharedKernel.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Charisma.Contracts.Data.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddContractsDataAccess(this IServiceCollection services)
        {
            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<IEventRepository, EfEventRepository<CharismaContractsDbContext>>();
            services.AddScoped<IEventSourcedRepository<Contract>, EventSourcedRepository<Contract>>();

            services.AddScoped<ICrudRepository<ContractReadModel>, EfCrudRepository<ContractReadModel, CharismaContractsDbContext>>();

            services.AddEntityFrameworkSqlServer().AddDbContext<CharismaContractsDbContext>(
                (serviceProvider, options) =>
                {
                    var configuration = serviceProvider.GetService<IConfiguration>();
                    var connectionString = configuration.GetConnectionString("DefaultConnection");
                    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Charisma.Contracts.Migrations"))
                        .UseInternalServiceProvider(serviceProvider);
                });
        }
    }
}
