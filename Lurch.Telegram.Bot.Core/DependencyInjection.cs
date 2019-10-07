using System;
using Microsoft.Extensions.DependencyInjection;

namespace Lurch.Telegram.Bot.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTelegramBot(this IServiceCollection services, TelegramBotConfiguration configuration)
        {
            services.AddSingleton<ITelegramBotService, TelegramBotService>();
            services.Configure<TelegramBotConfiguration>(options =>
            {
                options.BotToken = configuration.BotToken;
                options.Socks5Host = configuration.Socks5Host;
                options.Socks5Port = configuration.Socks5Port;
            });
            services.AddScoped<IHandleTelegramUpdate, TelegramUpdateHandler>();
            services.AddScoped<IHandleTelegramMessage, TelegramMessageHandler>();
            services.AddScoped<IHandleTelegramTextMessage, TelegramTextMessageHandler>();

            return services;
        }
    }
}
