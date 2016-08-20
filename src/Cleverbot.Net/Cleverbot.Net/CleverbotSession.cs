using System;
using System.Threading.Tasks;
using Cleverbot.Net.Io.Api;

namespace Cleverbot.Net
{
    /// <summary>
    /// A class representing a session with a cleverbot
    /// </summary>
    public class CleverbotSession
    {
        /// <summary>
        /// The user of the API
        /// </summary>
        private string ApiUser { get; set; }
        /// <summary>
        /// The API key
        /// </summary>
        private string ApiKey { get; set; }
        /// <summary>
        /// The nick of the bot in the current session
        /// </summary>
        public string BotNick { get; private set; }

        private CleverbotSession() {}

        /// <summary>
        /// Get a new bot using the <paramref name="apiUser"/> and <paramref name="apiKey"/>
        /// </summary>
        /// <param name="apiUser"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static async Task<CleverbotSession> NewSessionAsync(string apiUser, string apiKey)
        {
            var session = new CleverbotSession {ApiUser = apiUser, ApiKey = apiKey};
            var createResponse =
                await CleverbotCreateApi.GetResponseAsync(new CleverbotCreateRequest(session.ApiUser, session.ApiKey));
            if(createResponse.Status != "success") throw new Exception("The bot creation wasn't successful. Check your API credentials and try again. API returned status: " + createResponse.Status);
            session.BotNick = createResponse.Nick;
            return session;
        }

        /// <summary>
        /// Get a new bot using the <paramref name="apiUser"/> and <paramref name="apiKey"/>
        /// </summary>
        /// <param name="apiUser"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static CleverbotSession NewSession(string apiUser, string apiKey)
        {
            return NewSessionAsync(apiUser, apiKey).Result;
        }

        /// <summary>
        /// Send a question/message to the bot and get the response
        /// </summary>
        /// <param name="message">The message to be sent to the bot</param>
        /// <returns></returns>
        public async Task<string> SendAsync(string message)
        {
            var askResponse =
                await
                    CleverbotAskApi.GetResponseAsync(new CleverbotAskRequest(this.ApiUser, this.ApiKey, this.BotNick,
                        message));
            if(askResponse.Status != "success") throw new Exception("The API returned non-success status code: " + askResponse.Status);
            return askResponse.Response;
        }
        /// <summary>
        /// Send a question/message to the bot and get the response
        /// </summary>
        /// <param name="message">The message to be sent to the bot</param>
        /// <returns></returns>
        public string Send(string message)
        {
            return SendAsync(message).Result;
        }
    }
}
