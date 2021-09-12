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
            //// arrange
            const string botId = "aaaaa";
            const string userId = "bbbbb";
            var slackChannel = new Channel
            {
                Id = "ccccc"
            };

            var mappingConfigRepository = new Mock<IChannelMappingConfigRepository>();

            // mapping config not exists
            mappingConfigRepository.Setup(r => r.GetWithLineUserIdAsync(botId, userId))
                .ReturnsAsync(() => null);

            var slackService = new Mock<ISlackChannelService>();
            slackService.Setup(s => s.StartConversationAsync(userId))
                .ReturnsAsync(() => slackChannel);

            mappingConfigRepository.Setup(r => r.CreateAsync(botId, userId, slackChannel.Id))
                .ReturnsAsync(() => new ChannelMappingConfig
                {
                    SlackChannelId = slackChannel.Id,
                    LineUserId = userId,
                    LineBotId = botId,
                    IsDeleted = false
                });

            var lineFollowService = new LineFollowService(mappingConfigRepository.Object, slackService.Object);

            var source = new WebhookEventSource(EventSourceType.User, "sourceId", userId);
            var followEvent = new FollowEvent(source, 0, "");


            //// act
            var config = await lineFollowService.MakeChannelMappingConfigAsync(botId, followEvent);


            //// assert
            Assert.That(config, Is.Not.Null);
            Assert.That(config.SlackChannelId, Is.EqualTo(slackChannel.Id));
            Assert.That(config.LineBotId, Is.EqualTo(botId));
            Assert.That(config.LineUserId, Is.EqualTo(userId));
            Assert.That(config.IsDeleted, Is.False);
        }

        [Test]
        public async Task UpdateMappingConfigStateWhenAlreadyExists()
        {
            // arrange
            const string botId = "aaaaa";
            const string userId = "bbbbb";
            var slackChannel = new Channel
            {
                Id = "ccccc"
            };

            var deletedChannelMappingConfig = new ChannelMappingConfig
            {
                SlackChannelId = slackChannel.Id,
                LineBotId = botId,
                LineUserId = userId,
                IsDeleted = true
            };

            var mappingConfigRepository = new Mock<IChannelMappingConfigRepository>();

            // mapping config not exists
            mappingConfigRepository.Setup(r => r.GetWithLineUserIdAsync(botId, userId))
                .ReturnsAsync(() => deletedChannelMappingConfig);

            var slackService = new Mock<ISlackChannelService>();
            slackService.Setup(s => s.StartConversationAsync(userId))
                .ReturnsAsync(() => slackChannel);

            var lineFollowService = new LineFollowService(mappingConfigRepository.Object, slackService.Object);

            var source = new WebhookEventSource(EventSourceType.User, "sourceId", userId);
            var followEvent = new FollowEvent(source, 0, "");

            // assert
            Assert.That(deletedChannelMappingConfig, Is.Not.Null);
            Assert.That(deletedChannelMappingConfig.SlackChannelId, Is.EqualTo(slackChannel.Id));
            Assert.That(deletedChannelMappingConfig.LineBotId, Is.EqualTo(botId));
            Assert.That(deletedChannelMappingConfig.LineUserId, Is.EqualTo(userId));
            Assert.That(deletedChannelMappingConfig.IsDeleted, Is.False);
        }
    }
}
