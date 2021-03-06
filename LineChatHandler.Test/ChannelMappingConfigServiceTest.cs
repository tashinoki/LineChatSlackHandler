using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using LineChatSlackHandler.Models;
using LineChatSlackHandler.Repository;
using LineChatSlackHandler.Entity;
using LineChatSlackHandler.Services;
using Line.Messaging.Webhooks;

namespace LineChatSlackHandler.Test
{
    public class ChannelMappingConfigServiceTest
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public async Task MakeChannelMappingConfig_CreateWithWebhookEvent_WhenNotExists()
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

            var lineFollowService = new ChannelMappingConfigService(mappingConfigRepository.Object, slackService.Object);

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
        public async Task MakeMappingConfig_UpdateState_WhenAlreadyDeleted()
        {
            // arrange
            const string botId = "aaaaa";
            const string userId = "bbbbb";
            var slackChannel = new Channel
            {
                Id = "ccccc"
            };

            var deletedMappingConfig = new ChannelMappingConfig
            {
                SlackChannelId = slackChannel.Id,
                LineBotId = botId,
                LineUserId = userId,
                IsDeleted = true
            };

            var slackService = new Mock<ISlackChannelService>();
            var mappingConfigRepository = new Mock<IChannelMappingConfigRepository>();

            mappingConfigRepository.Setup(r => r.GetWithLineUserIdAsync(botId, userId))
                .ReturnsAsync(() => deletedMappingConfig);

            mappingConfigRepository.Setup(r => r.UpdateAsync(deletedMappingConfig))
                .ReturnsAsync(() => new ChannelMappingConfig
                {
                    SlackChannelId = slackChannel.Id,
                    LineBotId = botId,
                    LineUserId = userId,
                    IsDeleted = false
                });

            var lineFollowService = new ChannelMappingConfigService(mappingConfigRepository.Object, slackService.Object);

            var source = new WebhookEventSource(EventSourceType.User, "sourceId", userId);
            var followEvent = new FollowEvent(source, 0, "");


            //// act
            var config = await lineFollowService.MakeChannelMappingConfigAsync(botId, followEvent);


            // assert
            Assert.That(config, Is.Not.Null);
            Assert.That(config.SlackChannelId, Is.EqualTo(slackChannel.Id));
            Assert.That(config.LineBotId, Is.EqualTo(botId));
            Assert.That(config.LineUserId, Is.EqualTo(userId));
            Assert.That(config.IsDeleted, Is.False);
        }

        [Test]
        public async Task DeleteMappingConfig_UpdateState_WhenAlreadyExists()
        {
            // arrange
            const string botId = "aaaaa";
            const string userId = "bbbbb";
            var slackChannel = new Channel
            {
                Id = "ccccc"
            };

            var mappingConfig = new ChannelMappingConfig
            {
                SlackChannelId = slackChannel.Id,
                LineBotId = botId,
                LineUserId = userId,
                IsDeleted = false
            };

            var slackService = new Mock<ISlackChannelService>();
            var mappingConfigRepository = new Mock<IChannelMappingConfigRepository>();

            mappingConfigRepository.Setup(r => r.GetWithLineUserIdAsync(botId, userId))
                .ReturnsAsync(() => mappingConfig);

            mappingConfigRepository.Setup(r => r.UpdateAsync(mappingConfig))
                .ReturnsAsync(() => new ChannelMappingConfig
                {
                    SlackChannelId = slackChannel.Id,
                    LineBotId = botId,
                    LineUserId = userId,
                    IsDeleted = true
                });

            var lineFollowService = new ChannelMappingConfigService(mappingConfigRepository.Object, slackService.Object);

            var source = new WebhookEventSource(EventSourceType.User, "sourceId", userId);
            var unfollowEvent = new UnfollowEvent(source, 0);


            //// act
            await lineFollowService.DeleteChannelMappingConfigAsync(botId, unfollowEvent);


            // assert
            Assert.That(mappingConfig, Is.Not.Null);
            Assert.That(mappingConfig.SlackChannelId, Is.EqualTo(slackChannel.Id));
            Assert.That(mappingConfig.LineBotId, Is.EqualTo(botId));
            Assert.That(mappingConfig.LineUserId, Is.EqualTo(userId));
            Assert.That(mappingConfig.IsDeleted, Is.True);
        }

        [Test]
        public async Task DeleteMappingConfig_DoNothing_WhenConfigNotExists()
        {
            // arrange
            const string botId = "aaaaa";
            const string userId = "bbbbb";

            ChannelMappingConfig config = null;

            var slackService = new Mock<ISlackChannelService>();
            var mappingConfigRepository = new Mock<IChannelMappingConfigRepository>();

            mappingConfigRepository.Setup(r => r.GetWithLineUserIdAsync(botId, userId))
                .ReturnsAsync(() => config);

            var lineFollowService = new ChannelMappingConfigService(mappingConfigRepository.Object, slackService.Object);

            var source = new WebhookEventSource(EventSourceType.User, "sourceId", userId);
            var unfollowEvent = new UnfollowEvent(source, 0);


            //// act
            await lineFollowService.DeleteChannelMappingConfigAsync(botId, unfollowEvent);


            // assert
            Assert.That(config, Is.Null);
        }
    }
}
