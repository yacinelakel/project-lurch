namespace Lurch.Api.ApplicationServices.Common
{
    public class BotConfiguration
    {
        public static string Section = nameof(BotConfiguration);

        public string Socks5Host { get; set; }
        public string BotToken { get; set; }
        public int Socks5Port { get; set; }
    }
}