using Charisma.Invoices.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Charisma.Invoices.Migrations
{
    //dotnet ef migrations add InitialCreate
    //dotnet ef migrations remove
    //dotnet ef database update
    public class DbContextFactory : IDbContextFactory<CharismaInvoicesDbContext>
    {
        public CharismaInvoicesDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<CharismaInvoicesDbContext>();
            builder.UseSqlServer("Server=.;Database=Charisma_Invoices;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("Charisma.Invoices.Migrations"));
            return new CharismaInvoicesDbContext(builder.Options);
        }
    }
}
