using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace LineChatSlackHandler.Models
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class SlackMessage
    {
        public string Channel { get; set; }

        public string Text { get; set; }
    }

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class SlackFileMessage
    {
        public string Channel { get; set; }

        public Stream File { get; set; }
    }
}
