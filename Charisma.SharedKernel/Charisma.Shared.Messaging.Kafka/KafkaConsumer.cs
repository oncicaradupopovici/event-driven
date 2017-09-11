using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Charisma.SharedKernel.Messaging.Abstractions;

namespace Charisma.SharedKernel.Messaging.Kafka
{
    public class KafkaConsumer : IEventBusSubscriber
    {
        private readonly Consumer<string, string> _consumer;
        private readonly TopicRegistry _topicRegistry;

        public KafkaConsumer(IConfiguration configuration, TopicRegistry topicRegistry)
        {
            _topicRegistry = topicRegistry;

            var consumerGroup = configuration.GetSection("Kafka")["group.id"];
            var kafkaServers = configuration.GetSection("Kafka")["bootstrap.servers"];

            var config = new Dictionary<string, object>
            {
                {"group.id", consumerGroup},
                {"bootstrap.servers", kafkaServers},
                {"enable.auto.commit", "false"},
                {"auto.offset.reset", "earliest"}
            };
            _consumer = new Consumer<string, string>(config, new StringDeserializer(Encoding.UTF8),
                new StringDeserializer(Encoding.UTF8));
        }

        public async Task SubscribeAsync<TEvent>(Func<TEvent, Task> handler)
            where TEvent : IntegrationEvent
        {
            var topicName = _topicRegistry.GetTopicForEvent(typeof(TEvent));

            _consumer.OnPartitionsAssigned += (_, partitions) =>
            {
                Console.WriteLine($"Assigned partitions: [{string.Join(", ", partitions)}]");
                _consumer.Assign(partitions);

            };

            _consumer.OnPartitionsRevoked += (_, partitions) =>
            {
                Console.WriteLine($"Revoked partitions: [{string.Join(", ", partitions)}]");
                _consumer.Unassign();
            };

            _consumer.Subscribe(topicName);

            while (true)
            {
                if (_consumer.Consume(out Message<string, string> message, TimeSpan.FromMilliseconds(100)))
                {
                    await ProcessMessageAsync<TEvent>(message, handler);
                    await CommitAsync();
                }
            }
        }


        private async Task ProcessMessageAsync<TEvent>(Message<string, string> msg, Func<TEvent, Task> handler)
            where TEvent : IntegrationEvent
        {
            var @event = JsonConvert.DeserializeObject<TEvent>(msg.Value);
            await handler(@event);
        }

        private async Task CommitAsync()
        {
            await _consumer.CommitAsync();
        }

        
    }
}
