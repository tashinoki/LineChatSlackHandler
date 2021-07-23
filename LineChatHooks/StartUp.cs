using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using LineChatHooks.Services;

[assembly: FunctionsStartup(typeof(LineChatHooks.Startup))]
namespace LineChatHooks
{
    class Startup: FunctionsStartup
    {

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ILineChatService, LineChatService>();
        }
    }
}
