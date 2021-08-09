using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using LineChatSlackHandler.Models;
using LineChatSlackHandler.Services;
using Line.Messaging;
using Line.Messaging.Webhooks;
using LineChatSlackHandler.Factory;
using System.Net.Http;
using LineChatSlackHandler.Extensions;

namespace LineChatSlackHandler
{
    public class LineMessagingEventHandler
    {

        private readonly IChannelMappingService _channelMappingService;
        private readonly ILineChatService _lineChatService;
        private readonly ISlackService _slackService;
        private readonly IChannelMessageFactory _messageFactory;

        public LineMessagingEventHandler(
            IChannelMappingService channelMappingService,
            ILineChatService lineChatService,
            ISlackService slackService,
            IChannelMessageFactory messageFactory)
        {
            _channelMappingService = channelMappingService;
            _lineChatService = lineChatService;
            _slackService = slackService;
            _messageFactory = messageFactory;
        }

        [FunctionName("LineMessagingEventHandler")]
        public async Task Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestMessage req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            (var destination, var messageEvents) = await req.GetMessageEventsAsync(Environment.GetEnvironmentVariable("LineSecretToken"));

            var slackMessages = _messageFactory.CreateSlackMessages(destination, messageEvents);

            await _slackService.SendMessagesAsync(slackMessages);
            //var lineMessagingClient = new LineMessagingClient(Environment.GetEnvironmentVariable("LineAccessToken"));
            //try
            //{
            //    await lineMessagingClient.PushMessageAsync("U57853f60cf6cbc8db966086785a9f591", new List<ISendMessage> { new TextMessage("‚±‚ñ‚É‚¿‚Í") });
            //}
            //catch (Exception e)
            //{
            //    log.LogInformation(e.Message);
            //}
        
        }
    }
}