using Charisma.SharedKernel.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Charisma.Contracts.Application.Commands;
using Charisma.SharedKernel.Domain.Interfaces;
using Charisma.Payments.Domain.PaymentAggregate;

namespace Charisma.Payments.Application.CommandHandlers
{
    public class PaymentCommandHandlers : ICommandHandler<PayPayment>
    {
        private readonly IEventSourcedRepository<Payment> _repository;

        public PaymentCommandHandlers(IEventSourcedRepository<Payment> repository)
        {
            this._repository = repository;
        }

        
        public async Task HandleAsync(PayPayment command)
        {
            var payment = await this._repository.GetById(command.PaymentId);
            if (payment != null)
            {
                payment.Pay();
                await this._repository.SaveAsync(payment);
            }
        }
    }
}
