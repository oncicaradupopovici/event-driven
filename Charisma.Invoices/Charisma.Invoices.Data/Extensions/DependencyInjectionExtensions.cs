using Charisma.Invoices.Domain.InvoiceAggregate;
using Charisma.SharedKernel.Core;
using Charisma.SharedKernel.Core.Interfaces;
using Charisma.SharedKernel.Data;
using Charisma.SharedKernel.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Charisma.Invoices.Data.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddDataAccess(this IServiceCollection services)
        {
            services.AddScoped<ICrudRepository<Invoice>, EfCrudRepository<Invoice, CharismaInvoicesDbContext>>();
            services.Decorate<ICrudRepository<Invoice>, EfCrudRepositoryEventedDecorator<Invoice>>();

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
