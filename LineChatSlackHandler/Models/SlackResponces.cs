using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace LineChatSlackHandler.Models
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    internal class MessageChannelsEvent : ChallengeResponse
    {
        public string TeamId { get; set; }

        public string ApiAppId { get; set; }

        public SlackEvent Event { get; set; }

        public IReadOnlyList<string> AuthedTeams { get; set; }

        public string EventId { get; set; }

        public string EventTime { get; set; }
    }

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class SlackEvent
    {
        public string Message { get; set; }

        public string Channel { get; set; }

        public string User { get; set; }

        public string Text { get; set; }

        public string Ts { get; set; }

        public string EventTs { get; set; }

        public string ChannelType { get; set; }

        public string Type { get; set; }

        public SlackEventSubType Subtype { get; set; }

        public BotProfile BotProfile { get; set; }
    }

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class BotProfile
    {
        public string Id { get; set; }

        public bool Deleted { get; set; }

        public string Name { get; set; }

        public string TeamId { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
    public enum SlackEventSubType
    {
        Text,
        FileShare
    }

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class ApiResponse
    {
        public bool Ok { get; set; }

        public string? Error { get; set; }
    }

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class ConversationListResponse: ApiResponse
    {
        public IEnumerable<Channel> Channels { get; set; }
    }

    public class ConversationCreateResponse: ApiResponse
    {
        public Channel Channel { get; set; }
    }

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Channel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsMember { get; set; }

        public bool IsChannel { get; set; }

        public bool IsArchived { get; set; }
    }
}
