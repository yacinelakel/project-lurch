using System;
using Lurch.Telegram.Bot.Core;
using Lurch.Telegram.Bot.Core.Messages;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;

namespace Lurch.Telegram.Bot.UnitTests.Helpers
{
    public static class TelegramMessageFactory
    {
        public static TelegramMessage CreateFakeTelegramMessage(MessageType type)
        {
            var message = new Message();
            switch (type)
            {
                case MessageType.Unknown:
                    break;
                case MessageType.Text:
                    message.Text = "hello world";
                    break;
                case MessageType.Photo:
                    message.Photo = new PhotoSize[10];
                    break;
                case MessageType.Audio:
                    message.Audio = new Audio();
                    break;
                case MessageType.Video:
                    message.Video = new Video();
                    break;
                case MessageType.Voice:
                    message.Voice = new Voice();
                    break;
                case MessageType.Document:
                    message.Document = new Document();
                    break;
                case MessageType.Sticker:
                    message.Sticker = new Sticker();
                    break;
                case MessageType.Location:
                    message.Location = new Location();
                    break;
                case MessageType.Contact:
                    message.Contact = new Contact();
                    break;
                case MessageType.Venue:
                    message.Venue = new Venue();
                    break;
                case MessageType.Game:
                    message.Game = new Game();
                    break;
                case MessageType.VideoNote:
                    message.VideoNote = new VideoNote();
                    break;
                case MessageType.Invoice:
                    message.Invoice = new Invoice();
                    break;
                case MessageType.SuccessfulPayment:
                    message.SuccessfulPayment = new SuccessfulPayment();
                    break;
                case MessageType.WebsiteConnected:
                    message.ConnectedWebsite = "website";
                    break;
                case MessageType.ChatMembersAdded:
                    message.NewChatMembers = new User[1];
                    break;
                case MessageType.ChatMemberLeft:
                    message.LeftChatMember = new User();
                    break;
                case MessageType.ChatTitleChanged:
                    message.NewChatTitle = "title";
                    break;
                case MessageType.ChatPhotoChanged:
                    message.NewChatPhoto = new PhotoSize[1];
                    break;
                case MessageType.MessagePinned:
                    message.PinnedMessage = new Message();
                    break;
                case MessageType.ChatPhotoDeleted:
                    message.DeleteChatPhoto = true;
                    break;
                case MessageType.GroupCreated:
                    message.GroupChatCreated = true;
                    break;
                case MessageType.SupergroupCreated:
                    message.SupergroupChatCreated = true;
                    break;
                case MessageType.ChannelCreated:
                    message.ChannelChatCreated = true;
                    break;
                case MessageType.MigratedToSupergroup:
                    message.MigrateToChatId = 1;
                    break;
                case MessageType.MigratedFromGroup:
                    message.MigrateFromChatId = 1;
                    break;
                case MessageType.Poll:
                    message.Poll = new Poll();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return TelegramMessage.Create(new TelegramUpdate{Message = message});
        }
    }
}