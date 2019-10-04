using Telegram.Bot;

namespace Lurch.Api.ApplicationServices.Common
{
    public interface IBotService
    {
        TelegramBotClient Client { get; }
    }
}