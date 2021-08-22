using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging;

namespace LineChatSlackHandler.Services
{
    public interface ILineUserService
    {
        public Task<UserProfile> GetUserProfileAsync(string userId);
    }
}
