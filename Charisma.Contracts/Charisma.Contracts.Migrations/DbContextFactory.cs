using System;
using System.Collections.Generic;
using System.Text;
using Charisma.Contracts.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Charisma.Contracts.Migrations
{
    //dotnet ef migrations add InitialCreate
    //dotnet ef migrations remove
    //dotnet ef database update
    public class DbContextFactory : IDbContextFactory<CharismaContractsDbContext>
    {
        public CharismaContractsDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<CharismaContractsDbContext>();
            builder.UseSqlServer("Server=.;Database=Charisma_Contracts;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("Charisma.Contracts.Migrations"));
            return new CharismaContractsDbContext(builder.Options);
        }
    }
}
