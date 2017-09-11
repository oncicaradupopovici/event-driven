using Charisma.Payments.Domain.PayableAggregate;
using Charisma.SharedKernel.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Charisma.SharedKernel.Data.EntityFramework;
using Charisma.SharedKernel.Data.Abstractions;

namespace Charisma.Payments.Data.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddPaymentsDataAccess(this IServiceCollection services)
        {
            services.AddScoped<ICrudRepository<Payable>, EfEventedCrudRepository<Payable, CharismaPaymentsDbContext>>();

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
