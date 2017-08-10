using System.Threading.Tasks;
using Charisma.Contracts.Application.Commands;
using Charisma.Contracts.Domain.Aggregates;
using Charisma.SharedKernel.Application.Interfaces;
using Charisma.SharedKernel.Domain.Interfaces;

namespace Charisma.Contracts.Application.CommandHandlers
{
    public class ContractCommandHandlers : ICommandHandler<CreateContract>, ICommandHandler<UpdateContractAmount>
    {
        private readonly IEventSourcedRepository<Contract> _repository;
        public ContractCommandHandlers(IEventSourcedRepository<Contract> repository)
        {
            this._repository = repository;
        }

        public async Task HandleAsync(CreateContract message)
        {
            var contract = new Contract(message.Id, message.Amount, message.ClientId);
            await _repository.SaveAsync(contract);

        }

        public async Task HandleAsync(UpdateContractAmount message)
        {
            var contract = await _repository.GetById(message.Id);
            contract.UpdateAmount(message.NewAmount);
            await _repository.SaveAsync(contract, message.OriginalVersion);
        }
    }
}
