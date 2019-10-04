using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Lurch.Api.ApplicationServices.Common
{
    public interface IParseUpdateMessageService
    {
        Task ParseAsync(Update update);
    }

    public class ParseUpdateMessageService : IParseUpdateMessageService
    {
        public Task ParseAsync(Update update)
        {
            if(update == null  || update.Type != UpdateType.Message)
            {

            }

            return Task.CompletedTask;
        }
    }
}
