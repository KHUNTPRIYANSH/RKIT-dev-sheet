using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

namespace Kafka_Server.Services
{
    public class KafkaConsumerService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "gui-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe("gui-messages");

            while (!stoppingToken.IsCancellationRequested)
            {
                var message = consumer.Consume(stoppingToken);
                // Process message here (update UI, store in database, etc.)
                Console.WriteLine($"Received message: {message.Message.Value}");
            }

            return Task.CompletedTask;
        }
    }
}
