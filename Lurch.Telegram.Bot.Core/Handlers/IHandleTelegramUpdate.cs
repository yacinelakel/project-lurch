using System.Threading.Tasks;

namespace Lurch.Telegram.Bot.Core.Handlers
{
    public interface IHandleTelegramUpdate
    {
        Task HandleAsync(TelegramUpdate update);
    }
}