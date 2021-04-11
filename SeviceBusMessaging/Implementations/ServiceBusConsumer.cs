using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SeviceBusMessaging.Interfaces;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeviceBusMessaging.Implementations
{
   public class ServiceBusConsumer : IServiceBusConsumer
    {
        private readonly IProcessData _processData;
        private readonly IConfiguration _configuration;
        private readonly QueueClient _queueClient;
        private const string QUEUE_NAME = "myqueue";
        
        private string ServiceBus_Connection = "Endpoint=sb://dntservicebustestnamespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=WSy8pyiHNSd7+vKHgr68+grz8a2q51/Sz9/A0lQbekY=";
        public ServiceBusConsumer(IProcessData processData,
            IConfiguration configuration)
        {
            _processData = processData;
            _configuration = configuration;

            // ServiceBus_Connection= _configuration.GetConnectionString("ServiceBusConnectionString")
            _queueClient = new QueueClient(ServiceBus_Connection, QUEUE_NAME);
        }

        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var myPayload = JsonConvert.DeserializeObject<MyPayload>(Encoding.UTF8.GetString(message.Body));
            await _processData.Process(myPayload);
            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            return Task.CompletedTask;
        }

        public async Task CloseQueueAsync()
        {
            await _queueClient.CloseAsync();
        }
    }
}
