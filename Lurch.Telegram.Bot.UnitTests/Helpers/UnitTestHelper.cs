using System;
using System.Collections.Generic;
using Lurch.Telegram.Bot.Core;
using Lurch.Telegram.Bot.Core.Commands;
using Lurch.Telegram.Bot.Core.Messages;
using Moq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Lurch.Telegram.Bot.UnitTests.Helpers
{
    public static class UnitTestHelper
    {
        public static TelegramCommand CreateTelegramCommand(string messageText, long chatId = 42)
        {
            return new TelegramCommand(
                TelegramTextMessage.Create(
                    TelegramMessage.Create(
                        new TelegramUpdate
                        {
                            Message = new Message
                            {
                                Chat = new Chat
                                {
                                    Id = chatId
                                },
                                Text = messageText
                            }
                        })));
        }

        public static TelegramTextMessage CreateTelegramTextMessage(string messageText)
        {
            return TelegramTextMessage.Create(
                TelegramMessage.Create(
                    new TelegramUpdate
                    {
                        Message = new Message
                        {
                            Text = messageText
                        }
                    }));
        }

        public static void VerifySendTextMessageAsync(this Mock<ITelegramBotClient> fakeClient, long chatId,
            string text, Times times, ParseMode parseMode = ParseMode.Default)
        {
            fakeClient.Verify(x => x.SendTextMessageAsync(
                It.Is<ChatId>(id => id.Identifier == chatId),
                text,
                parseMode,
                false,
                false,
                0,
                null,
                default),
                times);
        }

        public static void VerifySendTextMessageAsync(this Mock<ITelegramBotClient> fakeClient, long chatId,
            string text)
        {
            fakeClient.VerifySendTextMessageAsync(chatId, text, Times.Once());
        }

        public static bool IsObsolete(this Enum value) {
            // https://stackoverflow.com/questions/29832536/check-if-enum-is-obsolete

            var fi = value.GetType().GetField(value.ToString());
            var attributes = (ObsoleteAttribute[])
                fi.GetCustomAttributes(typeof(ObsoleteAttribute), false);
            return (attributes.Length > 0);
        }

        public static IEnumerable<object[]> GetEnumerableValues<T>(Func<Enum, bool> condition = null) where T : Enum
        {
            // https://stackoverflow.com/questions/29832536/check-if-enum-is-obsolete
            foreach (var messageType in Enum.GetValues(typeof(T)))
            {
                var messageTypeEnum = messageType as Enum;

                if(condition == null || condition(messageTypeEnum))
                    yield return new[] { messageType };

            }
        }
    }
}