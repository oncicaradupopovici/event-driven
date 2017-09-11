using System;

namespace Charisma.SharedKernel.Messaging.Kafka
{
    public class TopicRegistry
    {
        public string GetTopicForEvent(Type eventType)
        {
            var type = eventType.Name;
            var topic = $"ch.events.{type}";
            return topic;
        }
    }
}
