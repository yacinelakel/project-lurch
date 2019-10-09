using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Lurch.Api.Controllers;
using Lurch.Telegram.Bot.Core;
using Lurch.Telegram.Bot.Core.Handlers;
using Lurch.Telegram.Bot.Core.Messages;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Lurch.Api.UnitTests
{
    public class TelegramControllerTests
    {
        [Fact]
        public async Task Post_should_call_service()
        {
            var fakeService = new Mock<IHandleTelegramUpdate>();
            var fakeUpdate = new TelegramUpdate{Id = 42};

            var controller = new TelegramController(fakeService.Object);

            await controller.Post(new TelegramUpdate {Id = 42});

            fakeService.Verify(x => x.HandleAsync(It.Is<TelegramUpdate>(u => u.Id == fakeUpdate.Id)), Times.Once);
        }

        [Fact]
        public async Task Post_should_return_status_code_200()
        {
            var fakeService = new Mock<IHandleTelegramUpdate>();
            var controller = new TelegramController(fakeService.Object);

            var result = await controller.Post(new TelegramUpdate());

            result.Should().BeAssignableTo<StatusCodeResult>();

            var statusCodeResult = result as StatusCodeResult;

            statusCodeResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
    }
}