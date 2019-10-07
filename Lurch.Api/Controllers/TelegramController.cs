using System;
using System.Threading.Tasks;
using Lurch.Telegram.Bot.Core;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Lurch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        private readonly IHandleTelegramUpdate _telegramUpdateHandler;
        private readonly ITelegramBotService _botService;

        public TelegramController(IHandleTelegramUpdate telegramUpdateHandler, ITelegramBotService botService)
        {
            _telegramUpdateHandler = telegramUpdateHandler;
            _botService = botService;
        }

        // TODO: Add a rand string suffix to route to avoid access 
        [HttpPost]
        public async Task<IActionResult> Post(TelegramUpdate update)
        {
            try
            {
                await _telegramUpdateHandler.HandleAsync(update);
            }
            catch (Exception e)
            {
                await _botService.Client.SendTextMessageAsync(new ChatId(392892505), $"```\n{e.ToString()}\n```", ParseMode.Markdown);
            }
            
            return Ok();
        }
    }
}