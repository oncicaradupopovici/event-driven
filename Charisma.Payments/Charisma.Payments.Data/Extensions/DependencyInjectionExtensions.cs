using Charisma.Payments.Domain.PayableAggregate;
using Charisma.SharedKernel.Core;
using Charisma.SharedKernel.Core.Interfaces;
using Charisma.SharedKernel.Data;
using Charisma.SharedKernel.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Charisma.Payments.Data.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddDataAccess(this IServiceCollection services)
        {
            services.AddScoped<ICrudRepository<Payable>, EfCrudRepository<Payable, CharismaPaymentsDbContext>>();
            services.Decorate<ICrudRepository<Payable>, EfCrudRepositoryEventedDecorator<Payable>>();

            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<IEventRepository, EfEventRepository<CharismaPaymentsDbContext>>();

            services.AddEntityFrameworkSqlServer().AddDbContext<CharismaPaymentsDbContext>(
                (serviceProvider, options) =>
                {
                    var configuration = serviceProvider.GetService<IConfiguration>();
                    var connectionString = configuration.GetConnectionString("DefaultConnection");
                    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Charisma.Payments.Migrations"))
                        .UseInternalServiceProvider(serviceProvider);
                });
        }
    }
}
