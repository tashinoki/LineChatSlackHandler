using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using LineChatSlackHandler.Models;
using LineChatSlackHandler.Factory;
using LineChatSlackHandler.Repository;
using LineChatSlackHandler.Entity;
using Line.Messaging;

namespace LineChatHandler.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task CreateLineTextMessageWithSlackEvent()
        {
            // arange
            const string skackChannelId = "aaaaa";
            const string text = "hello world";
            const string lineUseId = "aaaaa";

            var slackTextMessage = new SlackEvent
            {
                Channel = skackChannelId,
                Subtype = SlackEventSubType.Text,
                Text = text
            };

            var mappingConfigRepository = new Mock<IChannelMappingConfigRepository>();
            mappingConfigRepository.Setup(r => r.GetWithSlackChannelIdAsync(skackChannelId))
                .ReturnsAsync(() => new ChannelMappingConfig
                {
                    SlackChannelId = skackChannelId,
                    LineUserId = lineUseId
                });
            var lineMessageFactory = new LineMessageFactory(mappingConfigRepository.Object);

            // act
            var lineMessage = await lineMessageFactory.CreateAsync(slackTextMessage);
            
            // assert
            Assert.That(lineMessage.Message.Type, Is.EqualTo(MessageType.Text));
        }
    }
}