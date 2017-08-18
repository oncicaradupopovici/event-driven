using System;
using System.Collections.Generic;
using System.Text;
using Charisma.SharedKernel.Core.Interfaces;

namespace Charisma.SharedKernel.Domain
{
    public class Entity : IEntity
    {
        public Guid Id { get; protected set; }
    }
}
