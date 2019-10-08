using System;
using System.Threading.Tasks;
using Lurch.Telegram.Bot.Core.Messages;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types.Enums;

namespace Lurch.Telegram.Bot.Core.Handlers
{
    public class TelegramMessageHandler : IHandleTelegramMessage
    {
        private readonly ILogger<TelegramMessageHandler> _logger;
        private readonly IHandleTelegramTextMessage _textMessageHandler;

        public TelegramMessageHandler(ILogger<TelegramMessageHandler> logger, IHandleTelegramTextMessage textMessageHandler)
        {
            _logger = logger;
            _textMessageHandler = textMessageHandler;
        }

        public async Task HandleAsync(TelegramMessage message)
        {
            if (message == null)
                return;

            _logger.LogDebug("Message Received");

            switch (message.Type)
            {
                case MessageType.Unknown:
                    break;
                case MessageType.Text:
                    await _textMessageHandler.HandleAsync(TelegramTextMessage.Create(message));
                    break;
                case MessageType.Photo:
                    break;
                case MessageType.Audio:
                    break;
                case MessageType.Video:
                    break;
                case MessageType.Voice:
                    break;
                case MessageType.Document:
                    break;
                case MessageType.Sticker:
                    break;
                case MessageType.Location:
                    break;
                case MessageType.Contact:
                    break;
                case MessageType.Venue:
                    break;
                case MessageType.Game:
                    break;
                case MessageType.VideoNote:
                    break;
                case MessageType.Invoice:
                    break;
                case MessageType.SuccessfulPayment:
                    break;
                case MessageType.WebsiteConnected:
                    break;
                case MessageType.ChatMembersAdded:
                    break;
                case MessageType.ChatMemberLeft:
                    break;
                case MessageType.ChatTitleChanged:
                    break;
                case MessageType.ChatPhotoChanged:
                    break;
                case MessageType.MessagePinned:
                    break;
                case MessageType.ChatPhotoDeleted:
                    break;
                case MessageType.GroupCreated:
                    break;
                case MessageType.SupergroupCreated:
                    break;
                case MessageType.ChannelCreated:
                    break;
                case MessageType.MigratedToSupergroup:
                    break;
                case MessageType.MigratedFromGroup:
                    break;
                // Obselete
                case MessageType.Animation:
                    break;
                case MessageType.Poll:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}