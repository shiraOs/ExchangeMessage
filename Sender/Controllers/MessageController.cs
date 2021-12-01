using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Plain.RabbitMQ;

namespace Sender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private const string QUEUE_NAME = "my_queue";
        private readonly IPublisher publisher;

        public MessageController(IPublisher publisher)
        {
            this.publisher = publisher;
        }

        [HttpPost]
        public void Post([FromBody] MessageDetail message)
        {
            publisher.Publish(JsonConvert.SerializeObject(message), QUEUE_NAME, null);
        }
    }
}
