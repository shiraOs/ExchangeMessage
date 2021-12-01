using Microsoft.AspNetCore.Mvc;

namespace Receiver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiverController : ControllerBase
    {
        private readonly IReceiverStorage receiverStorage;

        public ReceiverController(IReceiverStorage receiverStorage)
        {
            this.receiverStorage = receiverStorage;
        }

        // GET: api/<ReceiverController>
        [HttpGet]
        public MessageDetail Get()
        {
            return receiverStorage.Message;
        }
    }
}
