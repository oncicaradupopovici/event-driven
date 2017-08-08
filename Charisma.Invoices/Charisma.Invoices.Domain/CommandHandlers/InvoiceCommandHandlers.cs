using System.Threading.Tasks;
using Charisma.Invoices.Domain.Aggregates;
using Charisma.Invoices.Domain.Commands;
using Charisma.SharedKernel.Core.Interfaces;
using Charisma.SharedKernel.Domain.Interfaces;

namespace Charisma.Invoices.Domain.CommandHandlers
{
    public class InvoiceCommandHandlers : ICommandHandler<CreateInvoice>, ICommandHandler<UpdateInvoiceAmount>
    {
        private readonly ICrudRepository<Invoice> _repository;
        public InvoiceCommandHandlers(ICrudRepository<Invoice> repository)
        {
            this._repository = repository;
        }

        public Task HandleAsync(CreateInvoice message)
        {
            var invoice = new Invoice(message.Id, message.ClientId, message.ContractId, message.Amount);
            return _repository.AddAsync(invoice);

        }

        public async Task HandleAsync(UpdateInvoiceAmount message)
        {
            var invoice = await _repository.GetSingleAsync(message.Id);
            invoice.UpdateAmount(message.NewAmount);
            await _repository.UpdateAsync(invoice);
        }
    }
}
