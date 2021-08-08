using System;
using System.Collections.Generic;
using System.Text;
using Line.Messaging;
using Line.Messaging.Webhooks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LineChatSlackHandler.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class LineWebhookRequest
    {
        public string Destination { get; set; }

        public IReadOnlyList<MessageEvent> Events { get; set; }
    }
}
