using System.Threading.Tasks;
using Lurch.Telegram.Bot.Core.Services;
using Microsoft.Extensions.Logging;

namespace Lurch.Telegram.Bot.Core.Commands
{
    public class EchoCommandExecutor : IExecuteTelegramCommand
    {
        private readonly ILogger<EchoCommandExecutor> _logger;
        private readonly ITelegramBotService _botService;
        private const string Command = "/echo";

        public EchoCommandExecutor(ILogger<EchoCommandExecutor> logger, ITelegramBotService botService)
        {
            _logger = logger;
            _botService = botService;
        }

        public bool CanExecute(TelegramCommand command)
        {
            return command.IsCommand && command.Command == Command && !string.IsNullOrWhiteSpace(command.Rest);
        }

        public async Task ExecuteCommand(TelegramCommand command)
        {
            //TODO: Maybe throw exception or log
            if (!CanExecute(command))
                return;

            await _botService.Client.SendTextMessageAsync(command.Message.Chat.Id, command.Rest);
        }
    }
}