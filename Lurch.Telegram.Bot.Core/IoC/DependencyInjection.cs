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
            services.AddSingleton<ITelegramBotService, TelegramBotService>();
            services.Configure<TelegramBotConfiguration>(options =>
            {
                options.BotToken = configuration.BotToken;
                options.Socks5Host = configuration.Socks5Host;
                options.Socks5Port = configuration.Socks5Port;
            });
            services.AddTransient<IHandleTelegramUpdate, TelegramUpdateHandler>();
            services.AddTransient<IHandleTelegramMessage, TelegramMessageHandler>();
            services.AddTransient<IHandleTelegramTextMessage, TelegramTextMessageHandler>();

            services.AddTransient<IExecuteTelegramCommand, EchoCommandExecutor>();

            return services;
        }
    }
}
