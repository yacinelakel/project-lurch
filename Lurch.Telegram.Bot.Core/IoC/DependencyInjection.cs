using System;
using Lurch.Telegram.Bot.Core.Commands;
using Lurch.Telegram.Bot.Core.Handlers;
using Lurch.Telegram.Bot.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Lurch.Telegram.Bot.Core.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTelegramBot(this IServiceCollection services, TelegramBotConfiguration configuration)
        {
            var config = configuration ?? throw new ArgumentNullException(nameof(configuration));
            services.AddSingleton<ITelegramBotService, TelegramBotService>();
            services.Configure<TelegramBotConfiguration>(options =>
            {
                options.BotToken = config.BotToken;
                options.Socks5Host = config.Socks5Host;
                options.Socks5Port = config.Socks5Port;
            });
            services.AddTransient<IHandleTelegramUpdate, TelegramUpdateHandler>();
            services.AddTransient<IHandleTelegramMessage, TelegramMessageHandler>();
            services.AddTransient<IHandleTelegramTextMessage, TelegramTextMessageHandler>();

            services.AddTransient<IExecuteTelegramCommand, EchoCommandExecutor>();

            return services;
        }
    }
}
