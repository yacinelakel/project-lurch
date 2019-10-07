using Telegram.Bot;

namespace Lurch.Telegram.Bot.Core
{
    public interface ITelegramBotService
    {
        TelegramBotClient Client { get; }
    }
}