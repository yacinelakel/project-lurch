using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Lurch.Api.IntegrationTests
{
    public class HeartbeatIntegrationTests : ApiIntegrationTests
    {
        private string ApiPath = $"{ApiRoot}/heartbeat";

        public HeartbeatIntegrationTests()
        {
            StartHost();
        }

        [Fact]
        public async Task Get_should_return_ok()
        {
            HttpResponseMessage response;
            using (var client = CreateClient())
            {
                response = await client.GetAsync(ApiPath);
            }

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}