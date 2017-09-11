using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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


            var eventProcessor = new Charisma.SharedKernel.Application.IntegrationEventProcessor(serviceProvider);

            Task.WaitAll(
                eventProcessor.ProcessEventAsync<ContractCreated>(),
                eventProcessor.ProcessEventAsync<ContractLineAdded>(),
                eventProcessor.ProcessEventAsync<ContractAmountUpdated>(),
                eventProcessor.ProcessEventAsync<ContractValidated>()
            );
        }

    }
}