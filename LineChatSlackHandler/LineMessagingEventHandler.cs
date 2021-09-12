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
        private IHandleLineWebhookService _handleLineWebhookService;

        public LineMessagingEventHandler(IHandleLineWebhookService handleLineWebhookService)
        {
            _handleLineWebhookService = handleLineWebhookService;
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
                await _handleLineWebhookService.HandleAsync(destination, webhookEvent);
            }
            return;        
        }
    }
}