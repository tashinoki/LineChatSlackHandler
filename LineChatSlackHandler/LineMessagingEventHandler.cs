using LineChatSlackHandler.Extensions;
using LineChatSlackHandler.Factory;
using LineChatSlackHandler.Services;
using Line.Messaging.Webhooks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LineChatSlackHandler
{
    public class LineMessagingEventHandler
    {

        private readonly IChannelMappingService _channelMappingService;
        private readonly ILineChatService _lineChatService;
        private readonly ISlackService _slackService;
        private readonly ISlackMessageFactory _messageFactory;

        public LineMessagingEventHandler(
            IChannelMappingService channelMappingService,
            ILineChatService lineChatService,
            ISlackService slackService,
            ISlackMessageFactory messageFactory)
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

            (var destination, var webhookEvents) = await req.GetMessageEventsAsync(Environment.GetEnvironmentVariable("LineSecretToken"));

            foreach(var webhookEvent in webhookEvents)
            {
                switch (webhookEvent.Type)
                {
                    case WebhookEventType.Follow:
                        return;
                    case WebhookEventType.Message:
                        var messageEvent = webhookEvent as MessageEvent;

                        if (messageEvent is null) throw new Exception("Line.Webhook.MessageEvent にキャストできませんでした。");
                        var slackMessage = await _messageFactory.CreateSlackMessageAsync(destination, messageEvent);
                        await _slackService.SendMessagesAsync(slackMessage);
                        return;
                    case WebhookEventType.Unfollow:
                        Console.WriteLine("hello world");
                        return;
                    default:
                        return;
                }
            }
            return;
            //var slackMessages = await _messageFactory.CreateSlackMessages(destination, webhookEvents);

            //await _slackService.SendMessagesAsync(slackMessages);
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