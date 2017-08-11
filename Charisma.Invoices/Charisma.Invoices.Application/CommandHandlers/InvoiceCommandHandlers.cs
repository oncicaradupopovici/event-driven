using System.Threading.Tasks;
using Charisma.Invoices.Application.Commands;
using Charisma.Invoices.Domain.InvoiceAggregate;
using Charisma.SharedKernel.Application.Interfaces;
using Charisma.SharedKernel.Domain.Interfaces;

namespace Charisma.Invoices.Application.CommandHandlers
{
    public class InvoiceCommandHandlers : ICommandHandler<CreateInvoice>, ICommandHandler<UpdateInvoiceAmount>
    {
        private readonly ICrudRepository<Invoice> _repository;
        public InvoiceCommandHandlers(ICrudRepository<Invoice> repository)
        {
            this._repository = repository;
        }

        public Task HandleAsync(CreateInvoice command)
        {
            var invoice = new Invoice(command.ClientId, command.ContractId, command.Amount);
            return _repository.AddAsync(invoice);

        }

        public async Task HandleAsync(UpdateInvoiceAmount message)
        {
            var invoice = await _repository.GetSingleAsync(message.InvoiceId);
            invoice.UpdateAmount(message.NewAmount);
            await _repository.UpdateAsync(invoice);
        }
    }
}
