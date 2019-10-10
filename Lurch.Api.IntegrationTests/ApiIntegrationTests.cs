using System;
using System.Net.Http;
using Lurch.Telegram.Bot.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Telegram.Bot;
using Xunit;

namespace Lurch.Api.IntegrationTests
{
    [Collection(nameof(ApiIntegrationTests))]
    public abstract class ApiIntegrationTests : IDisposable
    {
        protected const string ApiRoot = "api";

        private readonly Mock<ITelegramBotService> _fakeTelegramBotClient;

        private FakeConfiguration _configuration = FakeConfigurationExtensions.GetDefaultConfiguration();

        private TestServer _host;

        public ApiIntegrationTests()
        {
            _fakeTelegramBotClient = new Mock<ITelegramBotService>();
        }

        public void Dispose()
        {
            _host?.Dispose();
        }

        protected void StartHost(FakeConfiguration configuration = null)
        {
            if (configuration != null)
                _configuration = configuration;
            _host = CreateTestServer();
        }

        private TestServer CreateTestServer()
        {
            var webHostBuilder = CreateWebHostBuilder();
            return new TestServer(webHostBuilder);
        }

        private WebHostBuilder CreateWebHostBuilder()
        {
            var webHostBuilder = new WebHostBuilder();

            webHostBuilder
                .UseStartup<Startup>()
                .ConfigureTestServices(services =>
                    {
                        services.Replace(ServiceDescriptor.Singleton(typeof(ITelegramBotService),
                            _fakeTelegramBotClient.Object));
                    })
                .ConfigureAppConfiguration((_, config) =>
                {
                    config.AddInMemoryCollection(_configuration.ToInMemoryConfigurationProvider());
                });
            

            return webHostBuilder;
        }

        protected HttpClient CreateClient()
        {
            return _host.CreateClient();
        }
    }
}