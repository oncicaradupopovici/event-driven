using System;
using System.Collections.Generic;
using System.Text;

namespace Charisma.SharedKernel.Domain
{
    public abstract class ReadModelEntity
    {
        public Guid Id { get; protected set; }
    }
}
