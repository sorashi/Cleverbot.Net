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

#endregion

using System.Threading.Tasks;
using Cleverbot.Net.Com.Api;

namespace Cleverbot.Net
{
    public class CleverbotComSession
    {
        private CleverbotComSession() {
        }

        private string ApiKey { get; set; }

        /// <summary>
        ///     Holds the current session (is actually an encrypted version of the session history). Use it to inform the bot about
        ///     the context.
        /// </summary>
        public string CleverbotState { get; private set; }

        public static CleverbotComSession NewSession(string apiKey) {
            return NewSessionAsync(apiKey).Result;
        }

        public static async Task<CleverbotComSession> NewSessionAsync(string apiKey) {
            var session = new CleverbotComSession {ApiKey = apiKey};
            return await Task.FromResult(session);
        }

        public string Send(string message) {
            return SendAsync(message).Result;
        }

        public async Task<string> SendAsync(string message) {
            var result = await CleverbotComApi.GetReply(message, ApiKey, CleverbotState);
            CleverbotState = result.CleverbotState;
            return result.Output;
        }
    }
}