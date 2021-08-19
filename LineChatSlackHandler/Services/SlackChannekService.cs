using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace LineChatSlackHandler.Services
{
    public class SlackChannekService: ISlackChannelService
    {
        private ISlackApiClient _apiClient;

        public SlackChannekService(ISlackApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<string>> StartConversationAsync()
        {
            var channels = await _apiClient.FetchChannelListAsync();
            return channels.Select(c => c.Name);
        }

    }
}
