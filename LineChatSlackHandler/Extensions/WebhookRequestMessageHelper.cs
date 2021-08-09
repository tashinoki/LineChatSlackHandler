using Line.Messaging.Webhooks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LineChatSlackHandler.Extensions
{
    public static class WebhookRequestMessageHelper
    {
        public static async Task<(string, IReadOnlyList<MessageEvent>)> GetMessageEventsAsync(this HttpRequestMessage req, string channelSecret)
        {
            if (req is null)
                throw new ArgumentException(nameof(req));

            if (channelSecret is null)
                throw new ArgumentException(nameof(channelSecret));

            var content = await req.Content.ReadAsStringAsync();
            var xLineSignature = req.Headers.GetValues("X-Line-Signature").FirstOrDefault();
            if (string.IsNullOrEmpty(xLineSignature) || !VerifySignature(channelSecret, xLineSignature, content))
            {
                throw new Exception("Signature validation faild.");
            }

            if (!VerifySignature(channelSecret, xLineSignature, content))
            {
                throw new Exception("Signature validation faild.");
            }


            dynamic data = JsonConvert.DeserializeObject(content);
            string botId = data?.destination;

            if (botId is null)
                throw new ArgumentNullException("Bot User Idが NULL です。");

            var webhookEvents = WebhookEventParser.ParseEvents(data?.events) as IEnumerable<WebhookEvent>;
            var eventMessages = webhookEvents.Select(webhookEvent => webhookEvent as MessageEvent);
            return (botId, eventMessages.ToList().AsReadOnly());

            bool VerifySignature(string secret, string signature, string content)
            {
                try
                {
                    var key = Encoding.UTF8.GetBytes(secret);
                    var body = Encoding.UTF8.GetBytes(content);
                    using (HMACSHA256 hmac = new HMACSHA256(key))
                    {
                        var hash = hmac.ComputeHash(body, 0, body.Length);
                        var xLineBytes = Convert.FromBase64String(signature);
                        return SlowEquals(xLineBytes, hash);
                    }
                }
                catch
                {
                    return false;
                }
            }

            bool SlowEquals(byte[] a, byte[] b)
            {
                uint diff = (uint)a.Length ^ (uint)b.Length;
                for (int i = 0; i < a.Length && i < b.Length; i++)
                    diff |= (uint)(a[i] ^ b[i]);
                return diff == 0;
            }
        }
    }
}
