using System.Threading.Tasks;
using Lurch.Telegram.Bot.Core;
using Lurch.Telegram.Bot.Core.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Lurch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        private readonly IHandleTelegramUpdate _telegramUpdateHandler;

        public TelegramController(IHandleTelegramUpdate telegramUpdateHandler)
        {
            _telegramUpdateHandler = telegramUpdateHandler;
        }

        // TODO: Add a rand string suffix to route to avoid access 
        [HttpPost]
        public async Task<IActionResult> Post(TelegramUpdate update)
        {
            await _telegramUpdateHandler.HandleAsync(update);
            return Ok();
        }
    }
}