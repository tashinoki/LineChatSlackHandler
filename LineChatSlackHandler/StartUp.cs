using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using LineChatSlackHandler.Services;

[assembly: FunctionsStartup(typeof(HatenaBookmarkReminder.StartUp))]
namespace HatenaBookmarkReminder
{
    class StartUp: FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMessageMappingService, MessageMappingService>();
            services.AddSingleton<ILineChatService, LineChatService>();
            services.AddSingleton<ISlackService, SlackService>();
        }
    }
}
