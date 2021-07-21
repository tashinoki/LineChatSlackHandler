using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using LineChatHooks.Models;
using LineChatHooks.Services;

namespace LineChatHooks
{
    public static class HandleSlackChatMessage
    {
        private static readonly HashSet<string> SlackUsers = new HashSet<string>{ "U028T03BV3K" };
        private readonly ILi

        [FunctionName("HandleSlackChatMessage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<MessageChannelsEvent>(requestBody);
            var user = data.Event.User;

            //if (!IsSlackUserEvent(user)) return;

            return new OkObjectResult(data.Token);
        }

        private static bool IsSlackUserEvent(string User)
            => SlackUsers.Contains(User);
    }
}
