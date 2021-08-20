using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using LineChatSlackHandler.Models;
using System.Threading.Tasks;
using System.Linq;

namespace LineChatSlackHandler.Services
{
    public class SlackChannekService : ISlackChannelService
    {
        private ISlackApiClient _apiClient;

        private static ConcurrentDictionary<string, Channel> _cache = new ConcurrentDictionary<string, Channel>();

        public SlackChannekService(ISlackApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<string>> StartConversationAsync(string name)
        {
            var channels = await _apiClient.FetchChannelListAsync();

            var channel = channels.Where(c => c.Name == name).FirstOrDefault();

            if (channel is null)
            {

            }
            return channels.Select(c => c.Name);
        }

        private Task CreateChannel()
        {

        }

    }
}
