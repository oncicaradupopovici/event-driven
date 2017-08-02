using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Charisma.SharedKernel.Domain;
using Charisma.SharedKernel.Domain.Interfaces;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Charisma.SharedKernel.Messaging
{
    public class KafkaConsumer<TEvent> : IEventSubscriber<TEvent>
        where TEvent : Event
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

        public async Task SubscribeAsync(IEventHandler<TEvent> handler)
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


            foreach (var message in GetMessages())
            {
                await ProcessMessageAsync(message, handler);
                await CommitAsync();
            }
        }


        private IEnumerable<Message<string, string>> GetMessages()
        {
            while (true)
            {
                if (_consumer.Consume(out Message<string, string> msg, TimeSpan.FromMilliseconds(100)))
                    yield return msg;
            }
            // ReSharper disable once IteratorNeverReturns
        }


        private async Task ProcessMessageAsync(Message<string, string> msg, IEventHandler<TEvent> handler)
        {
            var @event = JsonConvert.DeserializeObject<TEvent>(msg.Value);
            await handler.HandleAsync(@event);
        }

        private async Task CommitAsync()
        {
            await _consumer.CommitAsync();
        }

        
    }
}
