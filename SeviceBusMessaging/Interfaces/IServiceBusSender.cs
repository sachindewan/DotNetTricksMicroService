using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeviceBusMessaging.Interfaces
{
    public interface IServiceBusSender
    {
        Task SendMessage(MyPayload payload);
    }
}
