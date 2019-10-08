using System;
using System.Threading.Tasks;
using Lurch.Telegram.Bot.Core.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Lurch.Telegram.Bot.Core.Handlers
{
    public class TelegramUpdateHandler : IHandleTelegramUpdate
    {
        private readonly ITelegramBotService _botService;
        private readonly TelegramBotConfiguration _configuration;
        private readonly ILogger<TelegramUpdateHandler> _logger;
        private readonly IHandleTelegramMessage _messageHandler;

        public TelegramUpdateHandler(ILogger<TelegramUpdateHandler> logger, IHandleTelegramMessage messageHandler,
            ITelegramBotService botService, IOptions<TelegramBotConfiguration> configuration)
        {
            _logger = logger;
            _messageHandler = messageHandler;
            _botService = botService;
            _configuration = configuration.Value;
        }

        public async Task HandleAsync(TelegramUpdate update)
        {
            try
            {
                await HandleUpdateAsync(update);
            }
            catch (Exception e)
            {
                if (_configuration.EnableExceptionForwarding)
                    //TODO: Create a properly formatted exception message
                    await _botService.Client
                        .SendTextMessageAsync(
                            new ChatId(_configuration.ExceptionChatId), 
                            $"```\n{e}\n```", 
                            ParseMode.Markdown);
                throw e;
            }
        }

        private async Task HandleUpdateAsync(TelegramUpdate update)
        {
            if (update == null)
                return;

            _logger.LogDebug("Update Received");
            switch (update.Type)
            {
                case UpdateType.Message:
                    await _messageHandler.HandleAsync(TelegramMessage.Create(update));
                    break;
                case UpdateType.Unknown:
                    break;
                case UpdateType.InlineQuery:
                    break;
                case UpdateType.ChosenInlineResult:
                    break;
                case UpdateType.CallbackQuery:
                    break;
                case UpdateType.EditedMessage:
                    break;
                case UpdateType.ChannelPost:
                    break;
                case UpdateType.EditedChannelPost:
                    break;
                case UpdateType.ShippingQuery:
                    break;
                case UpdateType.PreCheckoutQuery:
                    break;
                case UpdateType.Poll:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}