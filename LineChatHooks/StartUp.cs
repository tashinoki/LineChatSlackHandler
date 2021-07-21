using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LineChatHooks.Services;

[assembly: FunctionsStartup(typeof(LineChatHooks.StartUp))]
namespace LineChatHooks
{
    class StartUp: FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ILineChatService, LineChatService>();
        }
    }
}
