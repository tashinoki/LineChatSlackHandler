using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using LineChatSlackHandler.Models;
using LineChatSlackHandler.Factory;
using LineChatSlackHandler.Repository;
using LineChatSlackHandler.Entity;
using LineChatSlackHandler.Services;
using Line.Messaging;
using Line.Messaging.Webhooks;

namespace LineChatSlackHandler.Test
{
    public class LineFollowServiceTest
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public async Task CreateChannelMappingConfigWithWebhookEventWhenNotExists()
        {
            // arrange
            const string botId = "aaaaa";
            const string userId = "bbbbb";
            var slackChannel = new Channel
            {
                Id = "ccccc"
            };

            ChannelMappingConfig channelMappingConfig = null;

            var mappingConfigRepository = new Mock<IChannelMappingConfigRepository>();
            
            // mapping config not exists
            mappingConfigRepository.Setup(r => r.GetWithLineUserIdAsync(botId, userId))
                .ReturnsAsync(() => null);

            var slackService = new Mock<ISlackChannelService>();
            slackService.Setup(s => s.StartConversationAsync(userId))
                .ReturnsAsync(() => slackChannel);

            mappingConfigRepository.Setup(r => r.Create(botId, userId, slackChannel.Id))
                .Callback(() => CreateMappingConfig(botId, userId, slackChannel.Id));

            var lineFollowService = new LineFollowService(mappingConfigRepository.Object, slackService.Object);

            var source = new WebhookEventSource(EventSourceType.User, "sourceId", userId);
            var followEvent = new FollowEvent(source, 0, "");


            // act
            await lineFollowService.MakeChannelMappingConfigAsync(botId, followEvent);


            // assert
            Assert.That(channelMappingConfig, Is.Not.Null);
            Assert.That(channelMappingConfig.SlackChannelId, Is.EqualTo(slackChannel.Id));
            Assert.That(channelMappingConfig.LineBotId, Is.EqualTo(botId));
            Assert.That(channelMappingConfig.LineUserId, Is.EqualTo(userId));
            Assert.That(channelMappingConfig.IsDeleted, Is.False);

            void CreateMappingConfig(string botId, string userId, string channelId)
            {
                channelMappingConfig = new ChannelMappingConfig
                {
                    SlackChannelId = channelId,
                    LineUserId = userId,
                    LineBotId = botId,
                    IsDeleted = false
                };
            }
        }
    }
}
