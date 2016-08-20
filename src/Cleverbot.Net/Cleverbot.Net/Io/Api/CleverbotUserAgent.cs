using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Cleverbot.Net.Io.Api
{
    public class CleverbotUserAgent
    {
        public static async Task<string> PostAsync(string url, string postBody)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "*/*";
            request.ContentType = "application/json";
            request.UserAgent = "Cleverbot.Net / 1.0";
            var sw = new StreamWriter(await request.GetRequestStreamAsync());
            await sw.WriteAsync(postBody);
            await sw.FlushAsync();
            WebResponse response;
            try
            {
                response = await request.GetResponseAsync();
            }
            catch (WebException e)
            {
                if (e.Status != WebExceptionStatus.ProtocolError) throw;
                if (((HttpWebResponse) e.Response).StatusCode != HttpStatusCode.BadRequest) throw;
                string status;
                try
                {
                    var esr = new StreamReader(e.Response.GetResponseStream());
                    status = JObject.Parse(esr.ReadToEnd()).Value<string>("status");
                }
                catch (Exception ex)
                {
                    throw new Exception("An exception occured while trying to handle an exception.\n" + ex.Message);
                }
                throw new Exception(status);
            }
            var sr = new StreamReader(response.GetResponseStream());
            var result = await sr.ReadToEndAsync();
            sw.Dispose();
            sr.Dispose();
            response.Dispose();
            return result;
        }
    }
}
