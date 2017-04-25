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
    ///     The API class used to POST a message to the bot and get the response
    /// </summary>
    public static class CleverbotAskApi
    {
        private const string CleverbotAskApiUrl = @"https://cleverbot.io/1.0/ask";

        public static async Task<CleverbotAskResponse> GetResponseAsync(CleverbotAskRequest request) {
            var json = JsonConvert.SerializeObject(request);
            var result = await CleverbotUserAgent.PostAsync(CleverbotAskApiUrl, json, Cleverbot.CleverbotIo);
            return JsonConvert.DeserializeObject<CleverbotAskResponse>(result);
        }
    }

    /// <summary>
    ///     A JSON object, representing the ask request
    /// </summary>
    public class CleverbotAskRequest
    {
        public CleverbotAskRequest(string user, string key, string nick, string text) {
            User = user;
            Key = key;
            Nick = nick;
            Text = text;
        }

        [JsonProperty(PropertyName = "user")]
        public string User { get; }

        [JsonProperty(PropertyName = "key")]
        public string Key { get; }

        [JsonProperty(PropertyName = "nick")]
        public string Nick { get; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; }
    }

    /// <summary>
    ///     A JSON object representing the ask response
    /// </summary>
    public class CleverbotAskResponse
    {
        public CleverbotAskResponse(string status, string response) {
            Status = status;
            Response = response;
        }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; }

        [JsonProperty(PropertyName = "response")]
        public string Response { get; }
    }
}