using Charisma.SharedKernel.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Charisma.Payments.Application.Commands;
using Charisma.Payments.Domain.PayableAggregate;
using Charisma.SharedKernel.Domain.Interfaces;

namespace Charisma.Payments.Application.CommandHandlers
{
    public class PayableCommandHandlers : ICommandHandler<PayPayable>
    {
        private readonly ICrudRepository<Payable> _repository;

        public PayableCommandHandlers(ICrudRepository<Payable> repository)
        {
            this._repository = repository;
        }

        
        public async Task HandleAsync(PayPayable command)
        {
            var payable = await this._repository.GetSingleAsync(command.PayableId);
            if (payable != null)
            {
                payable.Pay();
                await this._repository.UpdateAsync(payable);
            }
        }
    }
}
