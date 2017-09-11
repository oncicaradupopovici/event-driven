using System;
using System.Collections.Generic;
using System.Text;

namespace Charisma.SharedKernel.Domain.Interfaces
{
    public interface IEventSourcedAggregateRoot : IEventedAggregateRoot
    {
        int Version { get; }
        void LoadFromHistory(IEnumerable<DomainEvent> history);
    }
}
