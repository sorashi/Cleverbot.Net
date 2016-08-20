using System;
using System.Threading.Tasks;
using Cleverbot.Net.Io.Api;

namespace Cleverbot.Net
{
    public class CleverbotSession
    {
        private string ApiUser { get; set; }
        private string ApiKey { get; set; }
        public string BotNick { get; private set; }

        private CleverbotSession() {}

        public static async Task<CleverbotSession> NewSessionAsync(string apiUser, string apiKey)
        {
            var session = new CleverbotSession {ApiUser = apiUser, ApiKey = apiKey};
            var createResponse =
                await CleverbotCreateApi.GetResponseAsync(new CleverbotCreateRequest(session.ApiUser, session.ApiKey));
            if(createResponse.Status != "success") throw new Exception("The bot creation wasn't successful. Check your API credentials and try again. API returned status: " + createResponse.Status);
            session.BotNick = createResponse.Nick;
            return session;
        }

        public static CleverbotSession NewSession(string apiUser, string apiKey)
        {
            return NewSessionAsync(apiUser, apiKey).Result;
        }

        public async Task<string> SendAsync(string message)
        {
            var askResponse =
                await
                    CleverbotAskApi.GetResponseAsync(new CleverbotAskRequest(this.ApiUser, this.ApiKey, this.BotNick,
                        message));
            if(askResponse.Status != "success") throw new Exception("The API returned non-success status code: " + askResponse.Status);
            return askResponse.Response;
        }

        public string Send(string message)
        {
            return SendAsync(message).Result;
        }
    }
}
