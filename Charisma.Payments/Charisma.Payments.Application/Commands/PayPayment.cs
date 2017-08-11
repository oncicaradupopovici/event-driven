using System;
using Charisma.SharedKernel.Application;

namespace Charisma.Contracts.Application.Commands
{
    public class PayPayment : Command
    {
        public Guid PaymentId { get; }

        public PayPayment(Guid paymentId)
        {
            this.PaymentId = paymentId;
        }
    }
}
