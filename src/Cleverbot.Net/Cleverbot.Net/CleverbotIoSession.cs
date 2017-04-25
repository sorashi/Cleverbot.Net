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

using Cleverbot.Net.Io.Api;
using System;
using System.Threading.Tasks;

namespace Cleverbot.Net
{
    public class CleverbotIoSession
    {
        private CleverbotIoSession() {
        }

        /// <summary>
        ///     The nick of the bot in the current session
        /// </summary>
        public string BotNick { get; private set; }

        /// <summary>
        ///     The API key
        /// </summary>
        private string ApiKey { get; set; }

        /// <summary>
        ///     The user of the API
        /// </summary>
        private string ApiUser { get; set; }

        /// <summary>
        ///     Get a new bot using the <paramref name="apiUser" /> and <paramref name="apiKey" />
        /// </summary>
        /// <param name="apiUser"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static CleverbotIoSession NewSession(string apiUser, string apiKey) {
            return NewSessionAsync(apiUser, apiKey).Result;
        }

        /// <summary>
        ///     Get a new bot using the <paramref name="apiUser" /> and <paramref name="apiKey" />
        /// </summary>
        /// <param name="apiUser"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static async Task<CleverbotIoSession> NewSessionAsync(string apiUser, string apiKey) {
            var session = new CleverbotIoSession {ApiUser = apiUser, ApiKey = apiKey};
            var createResponse =
                await CleverbotCreateApi.GetResponseAsync(new CleverbotCreateRequest(session.ApiUser, session.ApiKey));
            if (createResponse.Status != "success")
                throw new Exception(
                    "The bot creation wasn't successful. Check your API credentials and try again. API returned status: " +
                    createResponse.Status);
            session.BotNick = createResponse.Nick;
            return session;
        }

        /// <summary>
        ///     Send a question/message to the bot and get the response
        /// </summary>
        /// <param name="message">The message to be sent to the bot</param>
        /// <returns></returns>
        public string Send(string message) {
            return SendAsync(message).Result;
        }

        /// <summary>
        ///     Send a question/message to the bot and get the response
        /// </summary>
        /// <param name="message">The message to be sent to the bot</param>
        /// <returns></returns>
        public async Task<string> SendAsync(string message) {
            var askResponse =
                await
                    CleverbotAskApi.GetResponseAsync(new CleverbotAskRequest(ApiUser, ApiKey, BotNick,
                        message));
            if (askResponse.Status != "success")
                throw new Exception("The API returned non-success status code: " + askResponse.Status);
            return askResponse.Response;
        }
    }
}