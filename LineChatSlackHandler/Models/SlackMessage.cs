using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace LineChatSlackHandler.Models
{

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public abstract class SlackMessage
    {
        public string Channel { get; set; }

        public SlackMessageType Type { get; set; }
    }


    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class SlackTextMessage: SlackMessage
    {
        public string Text { get; set; }
    }


    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class SlackFileMessage: SlackMessage
    {

        public Stream File { get; set; }
    }


    public enum SlackMessageType
    {
        Text,
        File
    }
}
