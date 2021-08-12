using LineChatSlackHandler.Models;

namespace LineChatSlackHandler.Factory
{
    public interface ILineMessageFactory
    {
        public LineMessage Create(SlackEvent slackEvent);
    }
}
