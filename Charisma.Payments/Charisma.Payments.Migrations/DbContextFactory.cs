using System;
using System.Collections.Generic;
using System.Text;
using Charisma.Payments.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Charisma.Payments.Migrations
{
    //dotnet ef migrations add InitialCreate
    //dotnet ef migrations remove
    //dotnet ef database update
    public class DbContextFactory : IDbContextFactory<CharismaPaymentsDbContext>
    {
        public CharismaPaymentsDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<CharismaPaymentsDbContext>();
            builder.UseSqlServer("Server=.;Database=Charisma_Payments;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("Charisma.Payments.Migrations"));
            return new CharismaPaymentsDbContext(builder.Options);
        }
    }
}
