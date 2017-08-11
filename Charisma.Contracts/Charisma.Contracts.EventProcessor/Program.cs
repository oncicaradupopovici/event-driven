﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Charisma.Contracts.Data;
using Charisma.SharedKernel.Data;
using Charisma.SharedKernel.Domain.Interfaces;
using Charisma.SharedKernel.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Charisma.Contracts.PublishedLanguage.Events;

namespace Charisma.Contracts.EventProcessor
{
    public class Program
    {
        static void Main(string[] args)
        {
            var startup = new Startup();
            var services = new ServiceCollection()
                .AddLogging();

            startup.ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();
            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);


            var eventProcessor = new SharedKernel.EventProcessor.EventProcessor(serviceProvider);

            Task.WaitAll(
                eventProcessor.ProcessEventAsync<ContractCreated>(),
                eventProcessor.ProcessEventAsync<ContractLineAdded>(),
                eventProcessor.ProcessEventAsync<ContractAmountUpdated>()
            );
        }

    }
}