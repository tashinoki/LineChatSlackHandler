using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LineChatSlackHandler.Models
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class SlackMessage
    {
        public string Channel { get; set; }

        public string Text { get; set; }
    }
}
