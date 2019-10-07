using System.Threading.Tasks;

namespace Lurch.Telegram.Bot.Core
{
    public interface IHandleTelegramTextMessage
    {
        Task HandleAsync(TelegramTextMessage textMessage);
    }
}