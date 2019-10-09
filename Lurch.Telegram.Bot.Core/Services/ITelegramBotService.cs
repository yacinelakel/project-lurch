using Telegram.Bot;

namespace Lurch.Telegram.Bot.Core.Services
{
    public interface ITelegramBotService
    {
        ITelegramBotClient Client { get; }
    }
}