using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LineChatSlackHandler.Services
{
    public interface ISlackService
    {
        public Task SendMessage();
    }
}
