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
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://slack.com/api/"),
            DefaultRequestHeaders =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("SlackBotToken"))
            }
        };

        public Task SendMessagesAsync(SlackMessage message)
        {
            switch(message.Type)
            {
                case SlackMessageType.Text:
                    var textMessage = message as SlackTextMessage;
                    return PostMessageAsync(textMessage);

                case SlackMessageType.File:
                    var fileMessage = message as SlackFileMessage;
                    return UploadFileAsync(fileMessage);
                default:
                    throw new Exception("");
            }
        }

        private async Task PostMessageAsync(SlackTextMessage slackMessage)
        {
            var response = await _httpClient.PostAsJsonAsync("chat.postMessage", slackMessage);
            var result = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());

            if (!result.Ok)
                throw new Exception(result.Error);
        }

        private async Task UploadFileAsync(SlackFileMessage message)
        {
        }

        public async Task<string> CreateChannelAsync(string name)
        {
            var response = await _httpClient.PostAsJsonAsync("conversations.create", new Dictionary<string, string>
            {
                { "name", name }
            });
            var result = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());

            if (!result.Ok)
                throw new Exception(result.Error);

            return result.Error;
        }
    }
}
