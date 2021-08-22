using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.Webhooks;

namespace LineChatSlackHandler.Services
{
    public interface IHandleLineWebhookService
    {
        public Task HandleAsync(string botId, WebhookEvent webhookEvent);
    }
}
