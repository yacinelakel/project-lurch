using System.Threading.Tasks;

namespace Lurch.Telegram.Bot.Core
{
    public interface IHandleTelegramMessage
    {
        Task HandleAsync(TelegramMessage message);
    }
}