using LineChatSlackHandler.Models;
using System.Threading.Tasks;

namespace LineChatSlackHandler.Factory
{
    public interface ILineMessageFactory
    {
        public Task<LineMessage> CreateAsync(SlackEvent slackEvent);
    }
}
