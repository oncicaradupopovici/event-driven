using System.Threading.Tasks;
using Charisma.Contracts.Application.Commands;
using Charisma.Contracts.Domain.ContractAggregate;
using Charisma.SharedKernel.Application.Interfaces;
using Charisma.SharedKernel.Domain.Interfaces;

namespace Charisma.Contracts.Application.CommandHandlers
{
    public class ContractCommandHandlers : ICommandHandler<CreateContract>, ICommandHandler<AddContractLine>
    {
        private readonly IEventSourcedRepository<Contract> _repository;
        public ContractCommandHandlers(IEventSourcedRepository<Contract> repository)
        {
            this._repository = repository;
        }

        public async Task HandleAsync(CreateContract command)
        {
            var contract = new Contract(command.ClientId);
            await _repository.SaveAsync(contract);

        }

        public async Task HandleAsync(AddContractLine command)
        {
            var contract = await _repository.GetById(command.ContractId);
            contract.AddContractLine(command.Product, command.Price, command.Quantity);
            await _repository.SaveAsync(contract);
        }
    }
}
