using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeviceBusMessaging
{
    public interface IProcessData
    {
        Task Process(MyPayload myPayload);
    }
}
