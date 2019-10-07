using System;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Lurch.Telegram.Bot.Core
{
    public class TelegramTextMessage
    {
        private TelegramTextMessage(int updateId, Message message)
        {
            UpdateId = updateId;
            _message = message;
        }

        public static TelegramTextMessage Create(TelegramMessage message)
        {
            if (!IsTelegramTextMessage(message)) throw new Exception("Message is not a valid TelegramTextMessage");

            return new TelegramTextMessage(message.UpdateId, message.GetRawMessage());
        }

        public static bool IsTelegramTextMessage(TelegramMessage message)
        {
            return message != null && message.Type == MessageType.Text;
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
        /// The actual UTF-8 text of the message
        /// </summary>
        public string Text => _message.Text;

        /// <summary>
        /// Special entities like usernames, URLs, bot commands, etc. that appear in the text
        /// </summary>
        public MessageEntity[] Entities => _message.Entities;

        /// <summary>Gets the entity values.</summary>
        /// <value>The entity contents.</value>
        public IEnumerable<string> EntityValues => _message.EntityValues;

        public Message GetRawMessage() => _message;

    }
}