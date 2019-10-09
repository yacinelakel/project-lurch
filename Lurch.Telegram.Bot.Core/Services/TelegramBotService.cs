using System;
using Microsoft.Extensions.Options;
using MihaZupan;
using Telegram.Bot;

namespace Lurch.Telegram.Bot.Core.Services
{
    public class TelegramBotService : ITelegramBotService {
        public ITelegramBotClient Client { get; }

        public TelegramBotService(IOptions<TelegramBotConfiguration> options)
        {
            var config = options?.Value ?? throw new ArgumentNullException(nameof(options));
            Client = string.IsNullOrWhiteSpace(config.Socks5Host)
                ? new TelegramBotClient(config.BotToken)
                : new TelegramBotClient(config.BotToken,
                    new HttpToSocks5Proxy(config.Socks5Host, config.Socks5Port));
        }
    }
}