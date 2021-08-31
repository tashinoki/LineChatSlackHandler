using Line.Messaging.Webhooks;
using Line.Messaging;
using LineChatSlackHandler.Entity;
using LineChatSlackHandler.Models;
using LineChatSlackHandler.Repository;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LineChatSlackHandler.Factory
{
    public class SlackMessageFactory: ISlackMessageFactory
    {
        private readonly IChannelMappingConfigRepository _mappingConfigRepository;
        private readonly static HttpClient _httpClient = new HttpClient
        {
            DefaultRequestHeaders =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("LineAccessToken"))
            }
        };

        public SlackMessageFactory(IChannelMappingConfigRepository channelMappingConfigRepository)
        {
            _mappingConfigRepository = channelMappingConfigRepository;
        }

        public async Task<SlackMessage> CreateSlackMessageAsync(string lineBotId, MessageEvent messageEvent)
        {
            var mappingConfig = await _mappingConfigRepository.GetWithLineUserIdAsync(messageEvent.Source.UserId, lineBotId);
            var slackMessage = await CreateSlackMessageAsync(mappingConfig, messageEvent);

            return slackMessage;
        }

        private async ValueTask<SlackMessage> CreateSlackMessageAsync(ChannelMappingConfig mappingConfig, MessageEvent messageEvent)
        {
            switch(messageEvent.Message.Type)
            {
                case EventMessageType.Text:

                    return new SlackTextMessage {
                        Channel = mappingConfig.SlackChannelId,
                        Text = AttatchMention((messageEvent.Message as TextEventMessage)?.Text),
                        Type = SlackMessageType.Text
                    };

                case EventMessageType.Image:
                    var imageMessage = messageEvent.Message as MediaEventMessage;
                    try
                    {
                        var response = await _httpClient.GetAsync($"https://api-data.line.me/v2/bot/message/{imageMessage.Id}/content");
                        return new SlackFileMessage
                        {
                            Channel = mappingConfig.SlackChannelId,
                            File = await response.Content.ReadAsStreamAsync(),
                            Type = SlackMessageType.File
                        };
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.ToString());
                    }
                    
                default:
                    throw new Exception($"無効なLine Webhook Event {messageEvent.Message.Type} が指定されました。");
            }

            #region LocalFunction
            // Todo: remove hard coding
            string AttatchMention(string text) => $"<@U0226MT50F6>\n{text}";
            #endregion
        }
    }
}
