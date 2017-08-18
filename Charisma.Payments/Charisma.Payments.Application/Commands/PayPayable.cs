using System;
using Charisma.SharedKernel.Application;

namespace Charisma.Payments.Application.Commands
{
    public class PayPayable : Command
    {
        public Guid PayableId { get; }

        public PayPayable(Guid payableId)
        {
            this.PayableId = payableId;
        }
    }
}
