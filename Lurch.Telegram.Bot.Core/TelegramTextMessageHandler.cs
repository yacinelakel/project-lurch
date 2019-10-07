using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Lurch.Telegram.Bot.Core
{
    public class TelegramTextMessageHandler : IHandleTelegramTextMessage
    {
        private readonly ILogger<TelegramTextMessageHandler> _logger;

        public TelegramTextMessageHandler(ILogger<TelegramTextMessageHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(TelegramTextMessage textMessage)
        {
            if (textMessage == null)
                return;

            _logger.LogDebug("TextMessage Received");

            var text = textMessage.Text;

            if (text.Trim().StartsWith("/"))
            {
                var regex = new Regex("^\\s*(\\/w)\\s+(\\S+)");
                var match = regex.Match(text);
                // TODO: Do something
            } 
        }
    }

}