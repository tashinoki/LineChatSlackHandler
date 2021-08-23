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
        private ILineFollowService _lineFollowService;
        private ISlackMessageFactory _messageFactory;

        public HandleLineWebhookService(
            ISlackService slackService,
            ILineFollowService lineFollowService,
            ISlackMessageFactory messageFactory)
        {
            _slackService = slackService;
            _lineFollowService = lineFollowService;
            _messageFactory = messageFactory;
        }

        public async Task HandleAsync(string botId, WebhookEvent webhook)
        {
            switch(webhook.Type)
            {
                case WebhookEventType.Message:
                    var messageEvent = webhook as MessageEvent;

                    if (messageEvent is null)
                        throw new Exception("Line.Webhook.MessageEvent にキャストできませんでした。");

                    var slackMessage = await _messageFactory.CreateSlackMessageAsync(botId, messageEvent);
                    await _slackService.SendMessagesAsync(slackMessage);
                    new LineWebhookHandleResult { };
                    return;

                case WebhookEventType.Follow:
                    var followEvent = webhook as FollowEvent;

                    if (followEvent is null)
                        throw new Exception("Line.Webhook.FollowEvent にキャストできませんでした。");

                    await _lineFollowService.MakeChannelMappingConfigAsync(botId, followEvent);
                    return;

                default:
                    throw new Exception("");

            }
        }
    }
}
