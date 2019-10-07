using System.Threading.Tasks;

namespace Lurch.Telegram.Bot.Core
{
    public interface IHandleTelegramUpdate
    {
        Task HandleAsync(TelegramUpdate update);
    }
}