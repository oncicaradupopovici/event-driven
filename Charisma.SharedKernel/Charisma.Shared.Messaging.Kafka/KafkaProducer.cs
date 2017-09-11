using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Newtonsoft.Json;
using Charisma.SharedKernel.Messaging.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Charisma.SharedKernel.Messaging.Kafka
{
    public class KafkaProducer : IEventBusPublisher, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly Producer<string, string> _producer;
        private readonly TopicRegistry _topicRegistry;

        public KafkaProducer(IConfiguration configuration, TopicRegistry topicRegistry)
        {
            _configuration = configuration;
            _topicRegistry = topicRegistry;
            _producer = GetProducer();
        }

        public Task PublishAsync<TEvent>(TEvent @event) where TEvent : IntegrationEvent
        {
            var topicName = _topicRegistry.GetTopicForEvent(@event.GetType());
            var key = GetKeyForEvent(@event);
            var value = JsonConvert.SerializeObject(@event);
            return _producer.ProduceAsync(topicName, key, value);
        }


        private Producer<string,string> GetProducer()
        {
            var kafkaServers = _configuration.GetSection("Kafka")["bootstrap.servers"];
            var config = new Dictionary<string, object>
            {
                //{"bootstrap.servers", "10.1.3.166:19092,10.1.3.166:29092,10.1.3.166:39092"}
                {"bootstrap.servers", kafkaServers}
            };
            var producer = new Producer<string, string>(config, new StringSerializer(Encoding.UTF8),
                new StringSerializer(Encoding.UTF8));

            return producer;
        }


        private string GetKeyForEvent<TEvent>(TEvent @event) where TEvent : IntegrationEvent
        {
            return @event.EventId.ToString();
        }

        public void Dispose()
        {
            //_producer.Dispose();
        }
    }
}
