using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lurch.Telegram.Bot.Core.Commands;
using Lurch.Telegram.Bot.Core.Messages;
using Microsoft.Extensions.Logging;

namespace Lurch.Telegram.Bot.Core.Handlers
{
    public class TelegramTextMessageHandler : IHandleTelegramTextMessage
    {
        private readonly ILogger<TelegramTextMessageHandler> _logger;
        private readonly IEnumerable<IExecuteTelegramCommand> _commandLookup;

        public TelegramTextMessageHandler(ILogger<TelegramTextMessageHandler> logger, IEnumerable<IExecuteTelegramCommand> commandLookup)
        {
            _logger = logger;
            _commandLookup = commandLookup;
        }

        public async Task HandleAsync(TelegramTextMessage textMessage)
        {
            if (textMessage == null)
                return;

            _logger.LogDebug("TextMessage Received");


            await TryParseAsCommand(textMessage);
        }

        private async Task<bool> TryParseAsCommand(TelegramTextMessage textMessage)
        {
            var command = new TelegramCommand(textMessage);

            if (!command.IsCommand) return false;

            var applicableCommands = _commandLookup.Where(c => c.CanExecute(command));

            foreach (var commandExecutor in applicableCommands)
            {
                await commandExecutor.ExecuteCommand(command);
            }

            return true;
        }
    }
}