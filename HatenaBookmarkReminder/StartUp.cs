using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HatenaBookmarkReminder.Services;

[assembly: FunctionsStartup(typeof(HatenaBookmarkReminder.StartUp))]
namespace HatenaBookmarkReminder
{
    class StartUp: FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IHatenApiService, HatenaApiService>();
        }
    }
}
