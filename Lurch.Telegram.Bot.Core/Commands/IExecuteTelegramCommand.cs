using System.Threading.Tasks;

namespace Lurch.Telegram.Bot.Core.Commands
{
    public interface IExecuteTelegramCommand
    {
        bool CanExecute(TelegramCommand command);

        Task ExecuteCommand(TelegramCommand command);
    }
}