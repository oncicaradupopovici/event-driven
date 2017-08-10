using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Charisma.Contracts.Data;
using Charisma.Contracts.PublicContracts.Events;
using Charisma.Contracts.ReadModel.Entities;
using Charisma.SharedKernel.Application;
using Charisma.SharedKernel.Application.Interfaces;
using Charisma.SharedKernel.Core;
using Charisma.SharedKernel.Core.Interfaces;
using Charisma.SharedKernel.Data;
using Charisma.SharedKernel.Domain.Interfaces;
using Charisma.SharedKernel.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Charisma.SharedKernel.EventProcessor;
using Charisma.SharedKernel.ReadModel.Interfaces;
using Charisma.Contracts.Application.EventHandlers;
using Charisma.Contracts.Application.Extensions;

namespace Charisma.Contracts.EventProcessor
{
    public class Startup
    {
        private static IConfiguration _configuration;

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_configuration);
            services.AddApplication();


        }
    }
}
