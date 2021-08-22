using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging;
using LineChatSlackHandler.Models;

namespace LineChatSlackHandler.Services
{
    public class LineUserService: ILineUserService
    {
        private readonly static LineMessagingClient _messagingClient = new LineMessagingClient(
                Environment.GetEnvironmentVariable("LineAccessToken")
            );

        public Task<UserProfile> GetUserProfileAsync(string userId)
        {
            return _messagingClient.GetUserProfileAsync(userId);
        }
    }
}
