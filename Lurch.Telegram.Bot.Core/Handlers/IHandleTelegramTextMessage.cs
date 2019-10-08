using System.Threading.Tasks;
using Lurch.Telegram.Bot.Core.Messages;

namespace Lurch.Telegram.Bot.Core.Handlers
{
    public interface IHandleTelegramTextMessage
    {
        Task HandleAsync(TelegramTextMessage textMessage);
    }
}