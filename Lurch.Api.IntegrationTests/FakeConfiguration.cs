using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration.Memory;

namespace Lurch.Api.IntegrationTests
{
    public class FakeConfiguration
    {
        private readonly Dictionary<string, string> _configuration;

        public FakeConfiguration()
        {
            _configuration = new Dictionary<string, string>();
        }

        public void SetOrOverrideConfiguration(string key, string value)
        {
            _configuration[key] = value;
        }

        public MemoryConfigurationProvider ToInMemoryConfigurationProvider()
        {
            var source = new MemoryConfigurationSource {InitialData = _configuration.ToList()};
            return new MemoryConfigurationProvider(source);
        }
    }

    public static class FakeConfigurationExtensions {
        public static FakeConfiguration GetDefaultConfiguration()
        {
            var fake = new FakeConfiguration();

            fake.SetOrOverrideConfiguration("TelegramBotConfiguration:BotToken", "SomeBotToken");
            fake.SetOrOverrideConfiguration("TelegramBotConfiguration:BotToken", "Socks5Host");
            fake.SetOrOverrideConfiguration("TelegramBotConfiguration:BotToken", "0");
            fake.SetOrOverrideConfiguration("TelegramBotConfiguration:BotToken", "0");
            fake.SetOrOverrideConfiguration("TelegramBotConfiguration:BotToken", "false");

            return fake;
        }
    }
}