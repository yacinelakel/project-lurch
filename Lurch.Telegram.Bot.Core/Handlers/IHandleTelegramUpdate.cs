using System.Threading.Tasks;
using Lurch.Telegram.Bot.Core.Messages;

namespace Lurch.Telegram.Bot.Core.Handlers
{
    public interface IHandleTelegramUpdate
    {
        Task HandleAsync(TelegramUpdate update);
    }
}