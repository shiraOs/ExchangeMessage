using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Plain.RabbitMQ;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Receiver
{
    public class ReceiverData : IHostedService
    {
        private readonly ISubscriber subscriber;
        private readonly IReceiverStorage receiverStorage;

        public ReceiverData(ISubscriber subscriber, IReceiverStorage receiverStorage)
        {
            this.subscriber = subscriber;
            this.receiverStorage = receiverStorage;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            subscriber.Subscribe(ProcessMessage);
            return Task.CompletedTask;
        }

        private bool ProcessMessage(string message, IDictionary<string, object> headers)
        {
            receiverStorage.Message = JsonConvert.DeserializeObject<MessageDetail>(message);            
            return true;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
