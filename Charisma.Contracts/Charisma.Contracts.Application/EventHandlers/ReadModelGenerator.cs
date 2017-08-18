using System;
using System.Linq;
using System.Threading.Tasks;
using Charisma.SharedKernel.Core.Interfaces;
using Charisma.Contracts.ReadModel.Entities;
using Charisma.Contracts.PublishedLanguage.Events;
using Charisma.SharedKernel.Domain.Interfaces;

namespace Charisma.Contracts.Application.EventHandlers
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
            var c = await _contractReadModelRepository.GetSingleAsync(@event.AggregateId);
            if (c == null)
            {
                await _contractReadModelRepository.AddAsync(
                    new ContractReadModel(@event.AggregateId, @event.ClientId, @event.Version));
            }
        }

        public async Task HandleAsync(ContractAmountUpdated @event)
        {
            var e = await _contractReadModelRepository.GetSingleAsync(@event.AggregateId);

            //if(e == null)
            //    throw new Exception("Could not find entity in readModel");

            if (e != null)
            {
                e.Amount = @event.NewAmount;
                e.Version = @event.Version;
                await _contractReadModelRepository.UpdateAsync(e);
            }
        }

        public async Task HandleAsync(ContractLineAdded @event)
        {
            var e = await _contractReadModelRepository.GetSingleAsync(@event.AggregateId, "ContractLines");


            if (e != null)
            {
                if (e.ContractLines.All(cl => cl.Id != @event.ContractLineId))
                {
                    var contractLine = new ContractLineReadModel(@event.ContractLineId, @event.Product, @event.Price,
                        @event.Quantity, @event.AggregateId);
                    e.ContractLines.Add(contractLine);
                    e.Version = @event.Version;
                    await _contractReadModelRepository.UpdateAsync(e);
                }
            }
        }

        public async Task HandleAsync(ContractValidated @event)
        {
            var e = await _contractReadModelRepository.GetSingleAsync(@event.AggregateId);

            //if(e == null)
            //    throw new Exception("Could not find entity in readModel");

            if (e != null)
            {
                e.IsValidated = true;
                await _contractReadModelRepository.UpdateAsync(e);
            }
        }
    }
}
