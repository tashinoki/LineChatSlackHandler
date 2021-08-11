using LineChatSlackHandler.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LineChatSlackHandler.Factory
{
    public interface ILineMessageFactory
    {
        public void Create(SlackEvent slackEvent);
    }
}
