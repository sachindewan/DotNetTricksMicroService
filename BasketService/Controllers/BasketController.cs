using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeviceBusMessaging;
using SeviceBusMessaging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        IServiceBusSender _sender;
        public BasketController(IServiceBusSender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> InitiateOrder(MyPayload message)
        {
            // TO DO:
            var payload = new MyPayload { Message = message.Message, CreatedDate = DateTime.Now };
            await _sender.SendMessage(payload);
            return Ok();
        }
    }
}
