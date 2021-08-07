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
using LineChatSlackHandler.Models;
using LineChatSlackHandler.Services;

namespace LineChatSlackHandler
{
    public class HandleSlackChatMessage
    {
        private readonly IChannelMappingService _channelMappingService;
        private readonly ILineChatService _lineChatService;
        private readonly ISlackService _slackService;

        public HandleSlackChatMessage(
            IChannelMappingService channelMappingService,
            ILineChatService lineChatService,
            ISlackService slackService)
        {
            _channelMappingService = channelMappingService;
            _lineChatService = lineChatService;
            _slackService = slackService;
        }

        [FunctionName("HandleSlackChat")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<MessageChannelsEvent>(requestBody);

            if (!string.IsNullOrEmpty(data.Challenge))
            {
                return new OkObjectResult(data.Challenge);
            }

            var channelSwitch = _channelMappingService.GetWithSlackChannel(data.Event.Channel);
            var user = data.Event.User;

            return new OkObjectResult(data.Token);
        }
    }
}
