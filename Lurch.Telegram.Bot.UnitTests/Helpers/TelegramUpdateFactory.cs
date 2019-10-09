using System;
using Lurch.Telegram.Bot.Core;
using Lurch.Telegram.Bot.Core.Messages;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;

namespace Lurch.Telegram.Bot.UnitTests.Helpers
{
    public static class TelegramUpdateFactory
    {
        public static TelegramUpdate CreateFakeTelegramUpdate(UpdateType type)
        {
            var update = new TelegramUpdate();
            switch (type)
            {
                case UpdateType.Unknown:
                    break;
                case UpdateType.Message:
                    update.Message = new Message();
                    break;
                case UpdateType.InlineQuery:
                    update.InlineQuery = new InlineQuery();
                    break;
                case UpdateType.ChosenInlineResult:
                    update.ChosenInlineResult = new ChosenInlineResult();
                    break;
                case UpdateType.CallbackQuery:
                    update.CallbackQuery = new CallbackQuery();
                    break;
                case UpdateType.EditedMessage:
                    update.EditedMessage = new Message();
                    break;
                case UpdateType.ChannelPost:
                    update.ChannelPost = new Message();
                    break;
                case UpdateType.EditedChannelPost:
                    update.EditedChannelPost = new Message();
                    break;
                case UpdateType.ShippingQuery:
                    update.ShippingQuery = new ShippingQuery();
                    break;
                case UpdateType.PreCheckoutQuery:
                    update.PreCheckoutQuery = new PreCheckoutQuery();
                    break;
                case UpdateType.Poll:
                    update.Poll = new Poll();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return update;
        }
    }
}