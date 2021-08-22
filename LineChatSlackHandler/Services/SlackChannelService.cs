using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LineChatSlackHandler.Models;

namespace LineChatSlackHandler.Services
{
    public class SlackChannelService : ISlackChannelService
    {
        private ISlackApiClient _apiClient;
        private ILineUserService _lineUserService;

        public SlackChannelService(ISlackApiClient apiClient, ILineUserService lineUserService)
        {
            _apiClient = apiClient;
            _lineUserService = lineUserService;
        }

        // このメソッド名は呼び出しもとが戻り値を受け取れると想像しにくい気がする
        public async Task<Channel> StartConversationAsync(string lineUserId)
        {
            var profile = await _lineUserService.GetUserProfileAsync(lineUserId);
            var name = Regex.Replace(profile.DisplayName, "[\\s]", "");
            return  await _apiClient.CreateChannelAsync($"{name}-{lineUserId.ToLower()}");
        }
    }
}
