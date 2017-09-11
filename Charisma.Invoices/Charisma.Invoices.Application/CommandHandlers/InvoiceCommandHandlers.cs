using System.Threading.Tasks;
using Charisma.Invoices.Application.Commands;
using Charisma.Invoices.Domain.InvoiceAggregate;
using Charisma.SharedKernel.Application.Interfaces;
using Charisma.SharedKernel.Domain.Interfaces;

namespace Charisma.Invoices.Application.CommandHandlers
{
    public class InvoiceCommandHandlers : ICommandHandler<CreateInvoice>
    {
        private readonly ICrudRepository<Invoice> _repository;
        public InvoiceCommandHandlers(ICrudRepository<Invoice> repository)
        {
            this._repository = repository;
        }

        public async Task HandleAsync(CreateInvoice command)
        {
            var invoice = new Invoice(command.ClientId, command.ContractId, command.Amount);
            await _repository.AddAsync(invoice);
            await _repository.SaveChangesAsync();

        }
    }
}
