using Charisma.Invoices.Domain.InvoiceAggregate;
using Charisma.SharedKernel.Data.Abstractions;
using Charisma.SharedKernel.Data.EntityFramework;
using Charisma.SharedKernel.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Charisma.Invoices.Data.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddInvoicesDataAccess(this IServiceCollection services)
        {
            services.AddScoped<ICrudRepository<Invoice>, EfEventedCrudRepository<Invoice, CharismaInvoicesDbContext>>();

            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<IEventRepository, EfEventRepository<CharismaInvoicesDbContext>>();

            services.AddEntityFrameworkSqlServer().AddDbContext<CharismaInvoicesDbContext>(
                (serviceProvider, options) =>
                {
                    var configuration = serviceProvider.GetService<IConfiguration>();
                    var connectionString = configuration.GetConnectionString("DefaultConnection");
                    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Charisma.Invoices.Migrations"))
                        .UseInternalServiceProvider(serviceProvider);
                });
        }
    }
}
