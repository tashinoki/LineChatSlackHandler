using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Line.Messaging.Webhooks;
using LineChatSlackHandler.Factory;
using LineChatSlackHandler.Repository;
using LineChatSlackHandler.Models;
using LineChatSlackHandler.Entity;

namespace LineChatSlackHandler.Test
{
    public class SlackMessageFactoryTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task CreateSlackTextMessageWithLineTextMessage()
        {

            // arange
            const string slackChannelId = "aaaaa";
            const string text = "hello world";
            const string lineUseId = "aaaaa";
            const string lineBotId = "bbbbb";

            var mappingConfigRepository = new Mock<IChannelMappingConfigRepository>();
            mappingConfigRepository.Setup(r => r.GetWithLineUserIdAsync(lineUseId, lineBotId))
                .ReturnsAsync(() => new ChannelMappingConfig
                {
                    SlackChannelId = slackChannelId,
                    LineUserId = lineUseId
                });
            var slackMessageFactory = new SlackMessageFactory(mappingConfigRepository.Object);

            var source = new WebhookEventSource(EventSourceType.User, "sourceId", lineUseId);
            var eventMessage = new TextEventMessage("messageId", text);


            // act
            var slackMessage = await slackMessageFactory.CreateSlackMessageAsync(lineBotId, new MessageEvent(source, 0, eventMessage, "messageId"));


            // assert
            Assert.That(slackMessage.Type, Is.EqualTo(SlackMessageType.Text));
            Assert.That(slackMessage.Channel, Is.EqualTo(slackChannelId));

            var slackTextMessage = slackMessage as SlackTextMessage;

            Assert.That(slackTextMessage, Is.Not.Null);
            Assert.That(slackTextMessage.Text, Is.EqualTo($"<@U0226MT50F6>\n{text}"));
        }
    }
}
