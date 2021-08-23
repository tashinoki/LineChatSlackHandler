using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Line.Messaging.Webhooks;

namespace LineChatSlackHandler.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class LineWebhookHandleResult
    {
        public bool Success { get; set; }

        public WebhookEventType EventType { get; set; }

        public string? Error { get; set; }
    }
}
