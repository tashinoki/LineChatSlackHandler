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
        private IChannelMappingConfigService _channelMappingConfigService;
        private ISlackMessageFactory _messageFactory;

        public HandleLineWebhookService(
            ISlackService slackService,
            IChannelMappingConfigService channelMappingConfigService,
            ISlackMessageFactory messageFactory)
        {
            _slackService = slackService;
            _channelMappingConfigService = channelMappingConfigService;
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
                    return;

                case WebhookEventType.Follow:
                    var followEvent = webhook as FollowEvent;

                    if (followEvent is null)
                        throw new Exception("Line.Webhook.FollowEvent にキャストできませんでした。");

                    await _channelMappingConfigService.MakeChannelMappingConfigAsync(botId, followEvent);
                    return;

                case WebhookEventType.Unfollow:
                    var unfollowEvent = webhook as UnfollowEvent;

                    if (unfollowEvent is null)
                        throw new Exception("Line.Webhook.FollowEvent にキャストできませんでした。");

                    await _channelMappingConfigService.DeleteChannelMappingConfigAsync(botId, unfollowEvent);
                    return;
                default:
                    throw new Exception("");

            }
        }
    }
}
