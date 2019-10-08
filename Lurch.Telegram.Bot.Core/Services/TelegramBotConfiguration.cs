namespace Lurch.Telegram.Bot.Core.Services
{
    public class TelegramBotConfiguration
    {
        public string Socks5Host { get; set; }
        public string BotToken { get; set; }
        public int Socks5Port { get; set; }
        public int ExceptionChatId { get; set; }
        public bool EnableExceptionForwarding { get; set; }
    }
}