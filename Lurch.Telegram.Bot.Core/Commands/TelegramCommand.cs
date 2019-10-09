using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Lurch.Telegram.Bot.Core.Messages;

namespace Lurch.Telegram.Bot.Core.Commands
{
    public class TelegramCommand
    {
        public string CommandName { get; private set; }

        public IReadOnlyList<string> Args { get; private set; }

        public string Rest { get; private set; }

        public bool IsCommand { get; private set; }

        public TelegramTextMessage Message { get; }

        public TelegramCommand(TelegramTextMessage message)
        {
            Message = message;
            Parse();
        }

        private void Parse()
        {
            var trimmedText = Message.Text.TrimStart();
            // get command
            var regex = new Regex(@"^(\/\w+)(.*)");
            var match = regex.Match(trimmedText);

            if (!match.Success) return;

            IsCommand = true;
            CommandName = match.Groups[1].Value;
            Rest = match.Groups[2].Value;
            Args = Rest.Trim().Split(new[]{ " " }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}