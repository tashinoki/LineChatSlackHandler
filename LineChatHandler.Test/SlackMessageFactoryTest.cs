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
            const string messageId = "hogehoge";
            const string skackChannelId = "aaaaa";
            const string text = "hello world";
            const string lineUseId = "aaaaa";
            const string lineBotId = "bbbbb";

            var mappingConfigRepository = new Mock<IChannelMappingConfigRepository>();
            var slackMessageFactory = new SlackMessageFactory(mappingConfigRepository.Object);

            var source = new WebhookEventSource(EventSourceType.User, "", lineUseId);
            var eventMessage = new TextEventMessage("", text);
            var messageEvent = new MessageEvent(source, 0, eventMessage, messageId);

            // act
            var slackMessage = await slackMessageFactory.CreateSlackMessageAsync(lineBotId, messageEvent);

            // assert
            Assert.That(slackMessage.Type, Is.EqualTo(SlackMessageType.Text));
            Assert.That(slackMessage.Channel, Is.EqualTo(skackChannelId));

            var slackTextMessage = slackMessage as SlackTextMessage;

            Assert.That(slackTextMessage, Is.Not.Null);
            Assert.That(slackTextMessage.Text, Is.EqualTo(text));
        }
    }
}
