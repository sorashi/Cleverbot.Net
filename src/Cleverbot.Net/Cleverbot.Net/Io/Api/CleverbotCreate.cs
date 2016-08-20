using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cleverbot.Net.Io.Api
{
    /// <summary>
    ///     The API class used to request an assignment of a bot from the API
    /// </summary>
    public static class CleverbotCreateApi
    {
        private const string CleverbotCreateApiUrl = @"https://cleverbot.io/1.0/create";

        public static async Task<CleverbotCreateResponse> GetResponseAsync(CleverbotCreateRequest request)
        {
            var json = JsonConvert.SerializeObject(request,
                new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
            var result = await CleverbotUserAgent.PostAsync(CleverbotCreateApiUrl, json);
            return JsonConvert.DeserializeObject<CleverbotCreateResponse>(result);
        }
    }

    /// <summary>
    ///     A JSON object representing the request for the creation/assignment of a bot
    /// </summary>
    public class CleverbotCreateRequest
    {
        public CleverbotCreateRequest(string user, string key, string nick = null)
        {
            Key = key;
            Nick = nick;
            User = user;
        }

        [JsonProperty(PropertyName = "user")]
        public string User { get; }

        [JsonProperty(PropertyName = "key")]
        public string Key { get; }

        [JsonProperty(PropertyName = "nick")]
        public string Nick { get; }
    }

    /// <summary>
    ///     A JSON object representing the response of the creation/assignment of a bot
    /// </summary>
    public class CleverbotCreateResponse
    {
        public CleverbotCreateResponse(string nick, string status)
        {
            Nick = nick;
            Status = status;
        }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; }

        [JsonProperty(PropertyName = "nick")]
        public string Nick { get; }
    }
}