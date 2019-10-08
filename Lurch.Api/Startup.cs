using System;
using Lurch.Telegram.Bot.Core;
using Lurch.Telegram.Bot.Core.IoC;
using Lurch.Telegram.Bot.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lurch.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            CurrentEnvironment = env;
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        private IHostingEnvironment CurrentEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTelegramBot(GetTelegramBotConfiguration());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private TelegramBotConfiguration GetTelegramBotConfiguration()
        {
            const string telegramBotTokenEnvVarName = "TELEGRAM_BOT_TOKEN";
            const string telegramConfigurationKey = "TelegramBotConfiguration";

            if (!CurrentEnvironment.IsDevelopment())
                return Configuration.GetSection(telegramConfigurationKey).Get<TelegramBotConfiguration>();


            var botToken = Environment.GetEnvironmentVariable(telegramBotTokenEnvVarName);

            if (string.IsNullOrEmpty(botToken))
                throw new Exception("Cannot find token for telegram bot.");

            var botConfigSection = Configuration.GetSection(telegramConfigurationKey).Get<TelegramBotConfiguration>();
            botConfigSection.BotToken = botToken;
            return botConfigSection;

        }


    }
}