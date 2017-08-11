using System;
using System.Collections.Generic;
using System.Text;
using Charisma.SharedKernel.Core;

namespace Charisma.Payments.Domain.PaymentAggregate
{
    public class PaymentPayed : Event
    {
        public PaymentPayed(Guid eventId, Guid aggregateId)
            : base(eventId, aggregateId)
        {

        }
    }
}
