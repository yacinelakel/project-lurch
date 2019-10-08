using System.Threading.Tasks;

namespace Lurch.Telegram.Bot.Core.Handlers
{
    public interface IHandleTelegramMessage
    {
        Task HandleAsync(TelegramMessage message);
    }
}