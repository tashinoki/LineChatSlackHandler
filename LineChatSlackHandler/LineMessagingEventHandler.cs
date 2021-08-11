using LineChatSlackHandler.Extensions;
using LineChatSlackHandler.Factory;
using LineChatSlackHandler.Services;
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