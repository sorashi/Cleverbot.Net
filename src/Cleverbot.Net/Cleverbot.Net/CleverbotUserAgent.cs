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

using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Cleverbot.Net
{
    internal static class CleverbotUserAgent
    {
        private const string UserAgent = "Cleverbot.Net 1.0";

        public static async Task<string> GetAsync(string url, Cleverbot cleverbot) {
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "*/*";
            request.ContentType = "application/json";
            request.UserAgent = UserAgent;
            WebResponse response;
            try {
                response = await request.GetResponseAsync();
            } catch (WebException e) {
                throw await FormatWebException(e, cleverbot);
            }
            var sr = new StreamReader(response.GetResponseStream());
            var result = await sr.ReadToEndAsync();
            sr.Dispose();
            response.Dispose();
            return result;
        }

        /// <summary>
        ///     Does a HTTP POST request to the specified <paramref name="url" /> and with the specified
        ///     <paramref name="postBody" />
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postBody">The body of the request, containing application/json</param>
        /// <param name="cleverbot">The Cleverbot API to use</param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, string postBody, Cleverbot cleverbot) {
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "*/*";
            request.ContentType = "application/json";
            request.UserAgent = UserAgent;
            var sw = new StreamWriter(await request.GetRequestStreamAsync());
            await sw.WriteAsync(postBody);
            await sw.FlushAsync();
            WebResponse response;
            try {
                response = await request.GetResponseAsync();
            } catch (WebException e) {
                throw await FormatWebException(e, cleverbot);
            }
            var sr = new StreamReader(response.GetResponseStream());
            var result = await sr.ReadToEndAsync();
            sw.Dispose();
            sr.Dispose();
            response.Dispose();
            return result;
        }

        private static async Task<Exception> FormatWebException(WebException e, Cleverbot cleverbot) {
            if (e.Status != WebExceptionStatus.ProtocolError) throw e;
            var message =
                $"The {cleverbot} API returned a protocol error {(int) ((HttpWebResponse) e.Response).StatusCode}: ";
            try {
                var esr = new StreamReader(e.Response.GetResponseStream());
                var ejo = JObject.Parse(await esr.ReadToEndAsync());
                switch (cleverbot) {
                    case Cleverbot.CleverbotIo:
                        message += ejo.Value<string>("status");
                        break;

                    case Cleverbot.CleverbotCom:
                        message += ejo.Value<string>("error");
                        break;

                    default:
                        return new ArgumentOutOfRangeException(nameof(cleverbot), cleverbot, null);
                }
            } catch (Exception ex) {
                return new Exception("An exception occured while trying to handle an exception.\n" + ex.Message);
            }
            return new Exception(message);
        }
    }
}