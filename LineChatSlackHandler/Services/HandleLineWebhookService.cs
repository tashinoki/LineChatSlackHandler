using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LineChatSlackHandler.Models;
using Line.Messaging.Webhooks;
using LineChatSlackHandler.Factory;

namespace LineChatSlackHandler.Services
{
    public class HandleLineWebhookService: IHandleLineWebhookService
    {
        private ISlackService _slackService;
        private ISlackMessageFactory _messageFactory;

        public HandleLineWebhookService(
            ISlackService slackService,
            ISlackMessageFactory messageFactory)
        {
            _slackService = slackService;
            _messageFactory = messageFactory;
        }

        public async Task<LineWebhookHandleResult> HandleAsync(string botId, WebhookEvent webhook)
        {
            switch(webhook.Type)
            {
                case WebhookEventType.Message:
                    var messageEvent = webhook as MessageEvent;

                    if (messageEvent is null)
                        throw new Exception("Line.Webhook.MessageEvent にキャストできませんでした。");

                    var slackMessage = await _messageFactory.CreateSlackMessageAsync(botId, messageEvent);
                    await _slackService.SendMessagesAsync(slackMessage);
                    return new LineWebhookHandleResult { };
                default:
                    return new LineWebhookHandleResult { };

            }
            return new LineWebhookHandleResult{ };
        }
    }
}
