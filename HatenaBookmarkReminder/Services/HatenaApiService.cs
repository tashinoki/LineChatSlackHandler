using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Security.Cryptography;

namespace HatenaBookmarkReminder.Services
{
    public class HatenaApiService: IHatenApiService
    {
        private readonly HttpClient _httpClient = new HttpClient { };
        private const string RequestTokenUrl = "https://www.hatena.com/oauth/initiate";
        private readonly string _hatenaKey = HttpUtility.UrlEncode("yTVGWKqa6OiH5A%3D%3D");

        public async Task OAuthAsync()
        {
            var alg = SHA1.Create();
            var hash = Encoding.UTF8.GetString(alg.ComputeHash(Encoding.UTF8.GetBytes("yTVGWKqa6OiH5A==")));
            var request = new HttpRequestMessage(HttpMethod.Post, RequestTokenUrl);
            request.Headers.Add("ContentType", "application/x-www-form-urlencoded");
            request.Headers.Add("Authorization", $"OAuth oauth_callback=oob,oauth_consumer_key={_hatenaKey},oauth_nonce=0c670efea71547422662,oauth_signature=lvQC7AXTRIaqxbjwVGgPlYuNaaw%3D,oauth_signature_method=HMAC-SHA1,oauth_timestamp=1291689730,oauth_version=1.0");
            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
        
        
        }
    }
}
