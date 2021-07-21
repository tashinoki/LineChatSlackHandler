
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LineChatHooks.Models
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class ChallengeResponse
    {
        public string Token { get; set; }

        public string Challenge { get; set; }

        public string Type { get; set; }
    }
}
