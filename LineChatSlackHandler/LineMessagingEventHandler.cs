using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using LineChatSlackHandler.Models;
using LineChatSlackHandler.Services;
using Line.Messaging;
using Line.Messaging.Webhooks;

namespace LineChatSlackHandler
{
    public class LineMessagingEventHandler
    {

        private readonly IChannelMappingService _channelMappingService;
        private readonly ILineChatService _lineChatService;
        private readonly ISlackService _slackService;

        public LineMessagingEventHandler(
            IChannelMappingService channelMappingService,
            ILineChatService lineChatService,
            ISlackService slackService)
        {
            _channelMappingService = channelMappingService;
            _lineChatService = lineChatService;
            _slackService = slackService;
        }

        [FunctionName("LineMessagingEventHandler")]
        public async Task Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<LineWebhookRequest>(requestBody);

            if (data.Events.Count == 0)
            {
                log.LogInformation("イベントの数が0です。");
                return;
            }

            var mappings = data.Events.Select((MessageEvent messageEvent)
                => _channelMappingService.GetWithLineChannel(messageEvent.Source.UserId));

            await _slackService.SendMessage(data.Events);
            //var lineMessagingClient = new LineMessagingClient(Environment.GetEnvironmentVariable("LineAccessToken"));
            //try
            //{
            //    await lineMessagingClient.PushMessageAsync("U57853f60cf6cbc8db966086785a9f591", new List<ISendMessage> { new TextMessage("こんにちは") });
            //}
            //catch (Exception e)
            //{
            //    log.LogInformation(e.Message);
            //}
        }
    }
}