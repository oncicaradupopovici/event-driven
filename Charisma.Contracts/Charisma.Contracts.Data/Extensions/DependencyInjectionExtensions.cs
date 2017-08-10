using Charisma.Contracts.Domain.ContractAggregate;
using Charisma.Contracts.ReadModel.Entities;
using Charisma.SharedKernel.Core;
using Charisma.SharedKernel.Core.Interfaces;
using Charisma.SharedKernel.Data;
using Charisma.SharedKernel.Domain.Interfaces;
using Charisma.SharedKernel.ReadModel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Charisma.Contracts.Data.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddDataAccess(this IServiceCollection services)
        {
            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<IEventRepository, EfEventRepository<CharismaContractsDbContext>>();
            services.AddScoped<IEventSourcedRepository<Contract>, EventSourcedRepository<Contract>>();
            services.AddScoped<IReadModelRepository<ContractReadModel>, EfReadModelRepository<ContractReadModel, CharismaContractsDbContext>>();

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
