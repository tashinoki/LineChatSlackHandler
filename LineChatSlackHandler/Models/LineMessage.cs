using System;
using System.Collections.Generic;
using System.Text;
using Line.Messaging;

namespace LineChatSlackHandler.Models
{
    public class LineMessage
    {
        public string ToUserId { get; set; }

        public ISendMessage Message { get; set; }
    }
}
