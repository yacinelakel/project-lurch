using Microsoft.Extensions.Options;
using MihaZupan;
using Telegram.Bot;

namespace Lurch.Api.ApplicationServices.Common
{
    public class BotService : IBotService {
        public TelegramBotClient Client { get; }

        public BotService(IOptions<BotConfiguration> config)
        {
            var config1 = config.Value;
            Client = string.IsNullOrWhiteSpace(config1.Socks5Host)
                ? new TelegramBotClient(config1.BotToken)
                : new TelegramBotClient(config1.BotToken,
                    new HttpToSocks5Proxy(config1.Socks5Host, config1.Socks5Port));
        }
    }
}