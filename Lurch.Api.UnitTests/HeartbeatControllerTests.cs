using System.Net;
using FluentAssertions;
using Lurch.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Lurch.Api.UnitTests
{
    public class HeartbeatControllerTests
    {
        [Fact]
        public void Get_should_return_status_code_200()
        {
            var controller = new HeartbeatController();

            var result = controller.Get();

            result.Should().BeAssignableTo<StatusCodeResult>();

            var statusCodeResult = result as StatusCodeResult;

            statusCodeResult.StatusCode.Should().Be((int) HttpStatusCode.OK);
        }
    }
}