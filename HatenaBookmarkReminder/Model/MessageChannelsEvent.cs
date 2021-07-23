using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HatenaBookmarkReminder.Models
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    internal class MessageChannelsEvent: ChallengeResponse
    {
        public string TeamId { get; set; }

        public string ApiAppId { get; set; }

        public Event Event { get; set; }

        public IReadOnlyList<string> AuthedTeams { get; set; }

        public string EventId { get; set; }

        public string EventTime { get; set; }
    }

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    internal class Event
    {
        public string Message { get; set; }

        public string Channel { get; set; }

        public string User { get; set; }

        public string Text { get; set; }

        public string Ts { get; set; }

        public string EventTs { get; set; }

        public string ChannelType { get; set; }
    }
}
