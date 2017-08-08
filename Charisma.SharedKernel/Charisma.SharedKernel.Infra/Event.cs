using System;

namespace Charisma.SharedKernel.Core
{
    public class Event : Message
    {
        public Guid Id { get; protected set; }
        public int Version;
    }
}
