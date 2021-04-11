using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SeviceBusMessaging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeviceBusMessaging.Implementations
{
    public class ServiceBusSender : IServiceBusSender
    {
        private readonly QueueClient _queueClient;
        private readonly IConfiguration _configuration;
        private const string QUEUE_NAME = "myqueue";
        private string ServiceBus_Connection = "Endpoint=sb://dntservicebustestnamespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=WSy8pyiHNSd7+vKHgr68+grz8a2q51/Sz9/A0lQbekY=";
        public ServiceBusSender(IConfiguration configuration)
        {
            _configuration = configuration;
            // ServiceBus_Connection= _configuration.GetConnectionString("ServiceBusConnectionString")
            _queueClient = new QueueClient(ServiceBus_Connection, QUEUE_NAME);
        }
        public async Task SendMessage(MyPayload payload)
        {
            string data = JsonConvert.SerializeObject(payload);
            Message message = new Message(Encoding.UTF8.GetBytes(data));

            await _queueClient.SendAsync(message);
        }
    }
}
