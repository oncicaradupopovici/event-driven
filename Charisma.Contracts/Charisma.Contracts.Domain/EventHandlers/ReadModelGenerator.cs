using System;
using System.Threading.Tasks;
using Charisma.Contracts.Domain.ReadModel;
using Charisma.Contracts.PublicContracts.Events;
using Charisma.SharedKernel.Domain.Interfaces;

namespace Charisma.Contracts.Domain.EventHandlers
{
    public class ReadModelGenerator: 
        IEventHandler<ContractCreated>, 
        IEventHandler<ContractAmountUpdated>
    {
        private readonly IReadModelRepository<ContractReadModel> _contractReadModelRepository;

        public ReadModelGenerator(IReadModelRepository<ContractReadModel> contractReadModelRepository)
        {
            _contractReadModelRepository = contractReadModelRepository;
        }

        public async Task HandleAsync(ContractCreated @event)
        {
            await _contractReadModelRepository.AddAsync(
                new ContractReadModel(@event.Id, @event.Amount, @event.ClientId, @event.Version));

            await _contractReadModelRepository.SaveAsync();
        }

        public async Task HandleAsync(ContractAmountUpdated @event)
        {
            var e = await _contractReadModelRepository.GetSingleAsync(@event.Id);
            if(e == null)
                throw new Exception("Could not find entity in readModel");

            e.Amount = @event.NewAmount;
            e.Version = @event.Version;

            await _contractReadModelRepository.SaveAsync();
        }
    }
}
