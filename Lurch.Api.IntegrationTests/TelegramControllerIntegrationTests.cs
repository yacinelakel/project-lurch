using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Lurch.Telegram.Bot.Core;
using Lurch.Telegram.Bot.Core.Messages;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Lurch.Api.IntegrationTests
{
    public class TelegramControllerIntegrationTests : ApiIntegrationTests
    {
        private string ApiPath = $"{ApiRoot}/telegram";

        public TelegramControllerIntegrationTests()
        {
            StartHost();
        }

        [Fact]
        public async Task Post_should_return_ok()
        {
            
            HttpResponseMessage response;
            using (var client = CreateClient())
            {
                response = await client.PostAsJsonAsync(ApiPath, new TelegramUpdate());
            }

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}