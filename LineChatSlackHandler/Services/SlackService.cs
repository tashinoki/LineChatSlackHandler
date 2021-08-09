﻿using LineChatSlackHandler.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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

        public async Task SendMessagesAsync(IReadOnlyList<SlackMessage> messages)
        {
            foreach (var message in messages)
            {
                await PostMessageAsync(message);
            }
        }

        private async Task PostMessageAsync(SlackMessage slackMessage)
        {
            var response = await _httpClient.PostAsJsonAsync("chat.postMessage", slackMessage);
        }
    }
}
