using System.Threading.Tasks;
using Lurch.Api.ApplicationServices.Common;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace Lurch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        private readonly IParseUpdateMessageService _parseUpdateMessageService;

        public TelegramController(IParseUpdateMessageService parseUpdateMessageService)
        {
            _parseUpdateMessageService = parseUpdateMessageService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Update update)
        {
            await _parseUpdateMessageService.ParseAsync(update);
            return Ok();
        }
    }
}