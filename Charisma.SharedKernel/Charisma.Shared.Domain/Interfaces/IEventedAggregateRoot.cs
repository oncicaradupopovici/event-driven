using System;
using System.Collections.Generic;
using System.Text;

namespace Charisma.SharedKernel.Domain.Interfaces
{
    public interface IEventedAggregateRoot : IAggregateRoot
    {
        IEnumerable<DomainEvent> GetUncommittedChanges();
        void MarkChangesAsCommitted();
    }
}
