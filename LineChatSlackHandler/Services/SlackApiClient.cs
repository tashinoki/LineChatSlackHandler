using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LineChatSlackHandler.Models;
using Newtonsoft.Json;

namespace LineChatSlackHandler.Services
{
    public class SlackApiClient: ISlackApiClient
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://slack.com/api/"),
            DefaultRequestHeaders =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("SlackBotToken"))
            }
        };

        public async Task<IEnumerable<Channel>> FetchChannelListAsync()
        {
            var response = await _httpClient.GetAsync("conversations.list");

            var data = JsonConvert.DeserializeObject<ConversationList>(await response.Content.ReadAsStringAsync());

            if (data.Ok)
                return data.Channels;

            else
                throw new Exception(data.Error);
        }
    }
}
