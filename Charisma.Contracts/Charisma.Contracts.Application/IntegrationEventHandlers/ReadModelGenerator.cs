using System.Linq;
using System.Threading.Tasks;
using Charisma.Contracts.ReadModel.Entities;
using Charisma.Contracts.PublishedLanguage.Events;
using Charisma.SharedKernel.Domain.Interfaces;
using Charisma.SharedKernel.EventDrivenAbstractions;

namespace Charisma.Contracts.Application.IntegrationEventHandlers
{
    public class ReadModelGenerator: 
        IEventHandler<ContractCreated>, 
        IEventHandler<ContractAmountUpdated>,
        IEventHandler<ContractLineAdded>,
        IEventHandler<ContractValidated>
    {
        private readonly ICrudRepository<ContractReadModel> _contractReadModelRepository;

        public ReadModelGenerator(ICrudRepository<ContractReadModel> contractReadModelRepository)
        {
            _contractReadModelRepository = contractReadModelRepository;
        }

        public async Task HandleAsync(ContractCreated @event)
        {
            var c = await _contractReadModelRepository.GetSingleAsync(@event.ContractId);
            if (c == null)
            {
                await _contractReadModelRepository.AddAsync(
                    new ContractReadModel(@event.ContractId, @event.ClientId, @event.Version));
                await _contractReadModelRepository.SaveChangesAsync();
            }
        }

        public async Task HandleAsync(ContractAmountUpdated @event)
        {
            var e = await _contractReadModelRepository.GetSingleAsync(@event.ContractId);

            //if(e == null)
            //    throw new Exception("Could not find entity in readModel");

            if (e != null)
            {
                e.Amount = @event.NewAmount;
                e.Version = @event.Version;
                await _contractReadModelRepository.UpdateAsync(e);
                await _contractReadModelRepository.SaveChangesAsync();

            }
        }

        public async Task HandleAsync(ContractLineAdded @event)
        {
            var e = await _contractReadModelRepository.GetSingleAsync(@event.ContractId, "ContractLines");


            if (e != null)
            {
                if (e.ContractLines.All(cl => cl.Id != @event.ContractLineId))
                {
                    var contractLine = new ContractLineReadModel(@event.ContractLineId, @event.Product, @event.Price,
                        @event.Quantity, @event.ContractId);
                    e.ContractLines.Add(contractLine);
                    e.Version = @event.Version;
                    await _contractReadModelRepository.UpdateAsync(e);
                    await _contractReadModelRepository.SaveChangesAsync();
                }
            }
        }

        public async Task HandleAsync(ContractValidated @event)
        {
            var e = await _contractReadModelRepository.GetSingleAsync(@event.ContractId);

            //if(e == null)
            //    throw new Exception("Could not find entity in readModel");

            if (e != null)
            {
                e.IsValidated = true;
                await _contractReadModelRepository.UpdateAsync(e);
                await _contractReadModelRepository.SaveChangesAsync();
            }
        }
    }
}
