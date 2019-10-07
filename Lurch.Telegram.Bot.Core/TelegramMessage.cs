using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Lurch.Telegram.Bot.Core
{
    public class TelegramMessage
    {
        protected TelegramMessage(int updateId, Message message)
        {
            UpdateId = updateId;
            _message = message;
        }

        private readonly Message _message;

        /// <summary>Unique update identifier</summary>
        public int UpdateId { get; }

        /// <summary>Unique message identifier</summary>
        public int MessageId => _message.MessageId;

        public MessageType Type => _message.Type;

        /// <summary>Sender</summary>
        public User From => _message.From;

        /// <summary>Date the message was sent</summary>
        public DateTime Date => _message.Date;

        /// <summary>Conversation the message belongs to</summary>
        public Chat Chat => _message.Chat;

        /// <summary>
        /// Optional. For replies, the original message. Note that the Description object in this field will not contain further reply_to_message fields even if it itself is a reply.
        /// </summary>
        public Message ReplyToMessage => _message.ReplyToMessage;

        /// <summary>
        /// Optional. For text messages, the actual UTF-8 text of the message
        /// </summary>
        public string Text => _message.Text;

        /// <summary>
        /// Optional. For text messages, special entities like usernames, URLs, bot commands, etc. that appear in the text
        /// </summary>
        public MessageEntity[] Entities => _message.Entities;

        /// <summary>Gets the entity values.</summary>
        /// <value>The entity contents.</value>
        public IEnumerable<string> EntityValues => _message.EntityValues;

        public Message GetRawMessage() => _message;

        public static TelegramMessage Create(TelegramUpdate update)
        {
            if (!IsTelegramMessage(update)) throw new Exception("Update is not a valid TelegramMessage");

            return new TelegramMessage(update.Id, update.Message);
        }

        public static bool IsTelegramMessage(TelegramUpdate update)
        {
            return update != null && update.Type == UpdateType.Message && update.Message != null;
        }
    }
}