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
        private readonly IEnumerable<IExecuteTelegramCommand> _commandExecutors;

        public TelegramTextMessageHandler(ILogger<TelegramTextMessageHandler> logger, IEnumerable<IExecuteTelegramCommand> commandExecutors)
        {
            _logger = logger;
            _commandExecutors = commandExecutors;
        }

        public async Task HandleAsync(TelegramTextMessage textMessage)
        {
            if (textMessage == null)
                return;

            _logger.LogDebug("TextMessage Received");


            await TryParseAsCommand(textMessage);
        }

        private async Task TryParseAsCommand(TelegramTextMessage textMessage)
        {
            var command = new TelegramCommand(textMessage);

            if (!command.IsCommand) return;

            var applicableExecutors = _commandExecutors.Where(c => c.CanExecute(command));

            foreach (var commandExecutor in applicableExecutors)
            {
                await commandExecutor.ExecuteCommand(command);
            }
        }
    }
}