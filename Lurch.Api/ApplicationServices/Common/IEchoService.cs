using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Lurch.Api.ApplicationServices.Common
{
    public interface IEchoService
    {
        Task EchoAsync(Update update);
    }

    public class EchoService : IEchoService
    {
        private readonly IBotService _botService;
        private readonly ILogger<EchoService> _logger;

        public EchoService(IBotService botService, ILogger<EchoService> logger)
        {
            _botService = botService;
            _logger = logger;
        }
        public async Task EchoAsync(Update update)
        {
            if (update.Type != UpdateType.Message)
            {
                return;
            }

            var message = update.Message;

            _logger.LogInformation("Received Message from {0}", message.Chat.Id);

            if (message.Type == MessageType.Text)
            {
                // Echo each Message
                await _botService.Client.SendTextMessageAsync(message.Chat.Id, message.Text);
            }
        }
    }
}