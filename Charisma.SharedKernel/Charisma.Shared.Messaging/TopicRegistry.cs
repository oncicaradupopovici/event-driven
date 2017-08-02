using System;
using System.Collections.Generic;
using System.Text;
using Charisma.SharedKernel.Domain;

namespace Charisma.SharedKernel.Messaging
{
    public class TopicRegistry
    {
        public string GetTopicForCommand(Type commandType)
        {
            var type = commandType.Name;
            var topic = $"ch.commands.{type}";
            return topic;
        }

        public string GetTopicForEvent(Type eventType)
        {
            var type = eventType.Name;
            var topic = $"ch.events.{type}";
            return topic;
        }
    }
}
