using System.Threading.Tasks;
using Charisma.Invoices.PublishedLanguage.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Charisma.Payments.EventProcessor
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


            var eventProcessor = new SharedKernel.Application.IntegrationEventProcessor(serviceProvider);

            Task.WaitAll(
                eventProcessor.ProcessEventAsync<InvoiceCreated>()
            );
        }
    }
}