using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using LineChatSlackHandler.Services;
using LineChatSlackHandler.Repository;
using LineChatSlackHandler.Factory;

[assembly: FunctionsStartup(typeof(LineChatSlackHandler.StartUp))]
namespace LineChatSlackHandler
{
    class StartUp: FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IChannelMappingService, ChannelMappingService>();
            builder.Services.AddSingleton<ILineChatService, LineChatService>();
            builder.Services.AddSingleton<ISlackService, SlackService>();
            builder.Services.AddSingleton<IChannelMappingConfigRepository, ChannelMappingConfigRepository>();
            builder.Services.AddSingleton<IChannelMessageFactory, ChannelMessageFactory>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
        }
    }
}
