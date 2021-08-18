using LineChatSlackHandler.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        public async Task SendMessagesAsync(SlackMessage message)
        {
            await PostMessageAsync(message);
        }

        private async Task PostMessageAsync(SlackMessage slackMessage)
        {
            var response = await _httpClient.PostAsJsonAsync("chat.postMessage", slackMessage);
            var result = JsonConvert.DeserializeObject<SlackApiResponse>(await response.Content.ReadAsStringAsync());

            if (!result.Ok)
                throw new Exception(result.Error);
        }

        public async Task<string> CreateChannelAsync(string name)
        {
            var response = await _httpClient.PostAsJsonAsync("conversations.create", new { name  = name });
            var result = JsonConvert.DeserializeObject<SlackApiResponse>(await response.Content.ReadAsStringAsync());

            if (!result.Ok)
                throw new Exception(result.Error);

            return result.Error;
        }
    }
}
