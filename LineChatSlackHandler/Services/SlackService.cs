using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LineChatSlackHandler.Entity;
using Line.Messaging.Webhooks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LineChatSlackHandler.Services
{
    public class SlackService: ISlackService
    {
        private readonly IChannelMappingService _channelMappingService;
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://slack.com/api/"),
            DefaultRequestHeaders =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("SlackBotToken"))
            }
        };

        public SlackService(IChannelMappingService channelMappingService)
        {
            _channelMappingService = channelMappingService;
        }

        public async Task SendMessage(IReadOnlyList<MessageEvent> messageEvents)
        { 
            foreach(var messageEvent in messageEvents)
            {
                var channelSwith = _channelMappingService.GetWithLineChannel(messageEvent.Source.UserId);
            }

            var response = await _httpClient.PostAsJsonAsync("chat.postMessage", new Dictionary<string, string>
            {
                { "channel", "C02B6KW4K4G" },
                { "text", "hello" }
            });
    }
}
