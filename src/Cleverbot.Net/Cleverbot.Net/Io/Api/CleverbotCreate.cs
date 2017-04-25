#region LICENSE

// MIT License
//
// Copyright (c) 2017 Sorashi
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#endregion LICENSE

using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Cleverbot.Net.Io.Api
{
    /// <summary>
    ///     The API class used to request an assignment of a bot from the API
    /// </summary>
    public static class CleverbotCreateApi
    {
        private const string CleverbotCreateApiUrl = @"https://cleverbot.io/1.0/create";

        public static async Task<CleverbotCreateResponse> GetResponseAsync(CleverbotCreateRequest request) {
            var json = JsonConvert.SerializeObject(request,
                new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
            var result = await CleverbotUserAgent.PostAsync(CleverbotCreateApiUrl, json, Cleverbot.CleverbotIo);
            return JsonConvert.DeserializeObject<CleverbotCreateResponse>(result);
        }
    }

    /// <summary>
    ///     A JSON object representing the request for the creation/assignment of a bot
    /// </summary>
    public class CleverbotCreateRequest
    {
        public CleverbotCreateRequest(string user, string key, string nick = null) {
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
        public CleverbotCreateResponse(string nick, string status) {
            Nick = nick;
            Status = status;
        }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; }

        [JsonProperty(PropertyName = "nick")]
        public string Nick { get; }
    }
}