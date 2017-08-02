using System;
using System.Linq;
using System.Threading.Tasks;
using Charisma.Contracts.PublicContracts.Events;
using Charisma.SharedKernel.Domain;
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


            var eventTypes = new[] {typeof(ContractCreated), typeof(ContractAmountUpdated)};
            var eventProcessor = new SharedKernel.EventProcessor.EventProcessor(serviceProvider);

            eventProcessor.ProcessEventsAsync(eventTypes).Wait();

        }
    }
}