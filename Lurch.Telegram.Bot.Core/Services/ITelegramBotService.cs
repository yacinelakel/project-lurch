using Telegram.Bot;

namespace Lurch.Telegram.Bot.Core.Services
{
    public interface ITelegramBotService
    {
        TelegramBotClient Client { get; }
    }
}