﻿using System;
using Microsoft.Extensions.Options;
using MihaZupan;
using Telegram.Bot;

namespace Lurch.Telegram.Bot.Core.Services
{
    public class TelegramBotService : ITelegramBotService {
        public TelegramBotClient Client { get; }

        public TelegramBotService(IOptions<TelegramBotConfiguration> config)
        {
            var config1 = config?.Value ?? throw new ArgumentNullException(nameof(config));
            Client = string.IsNullOrWhiteSpace(config1.Socks5Host)
                ? new TelegramBotClient(config1.BotToken)
                : new TelegramBotClient(config1.BotToken,
                    new HttpToSocks5Proxy(config1.Socks5Host, config1.Socks5Port));
        }
    }
}