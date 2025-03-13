using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace Kafka_Server.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessageController : ControllerBase
    {
        private readonly IProducer<Null, string> _producer;

        public MessageController()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] string message)
        {
            await _producer.ProduceAsync("gui-messages", new Message<Null, string> { Value = message });
            return Ok();
        }
    }
}
