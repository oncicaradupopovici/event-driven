using System;
using System.Linq;
using System.Threading.Tasks;
using Charisma.Contracts.PublishedLanguage.Events;
using Charisma.Invoices.Data;
using Charisma.SharedKernel.Domain;
using Charisma.SharedKernel.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Charisma.Invoices.EventProcessor
{
    class Program
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
                eventProcessor.ProcessEventAsync<ContractAmountUpdated>()
            );

        }
    }
}