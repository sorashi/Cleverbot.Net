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
using System;
using System.Text;
using System.Threading.Tasks;

namespace Cleverbot.Net.Com.Api
{
    public static class CleverbotComApi
    {
        private const string ApiUrl = @"https://www.cleverbot.com/getreply";

        /// <summary>
        ///     Returns a reply from the bot
        /// </summary>
        /// <param name="input"></param>
        /// <param name="apiKey"></param>
        /// <param name="cs">Cleverbot state</param>
        /// <returns></returns>
        public static async Task<CleverbotComObject> GetReply(string input, string apiKey, string cs = null) {
            var sb = new StringBuilder(ApiUrl);
            sb.Append($"?input={input}&key={apiKey}");
            if (!string.IsNullOrEmpty(cs))
                sb.Append($"&cs={cs}");
            return JsonConvert.DeserializeObject<CleverbotComObject>(
                await CleverbotUserAgent.GetAsync(sb.ToString(), Cleverbot.CleverbotCom), new JsonSerializerSettings {
                    NullValueHandling = NullValueHandling.Ignore
                });
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class CleverbotComObject
    {
        [JsonProperty(PropertyName = "cs")]
        public string CleverbotState { get; set; }

        [JsonProperty(PropertyName = "input")]
        public string Input { get; set; }

        [JsonProperty(PropertyName = "output")]
        public string Output { get; set; }

        [JsonProperty(PropertyName = "interaction_count")]
        public int InteractionCount { get; set; }

        [JsonProperty(PropertyName = "input_other")]
        public string InputOther { get; set; }

        [JsonProperty(PropertyName = "predicted_input")]
        public string PredictedInput { get; set; }

        [JsonProperty(PropertyName = "accuracy")]
        public int Accuracy { get; set; }

        [JsonProperty(PropertyName = "output_label")]
        public string OutputLabel { get; set; }

        [JsonProperty(PropertyName = "conversation_id")]
        public string ConversationId { get; set; }

        [JsonProperty(PropertyName = "errorline")]
        public string ErrorLine { get; set; }

        [JsonProperty(PropertyName = "database_version")]
        public int DatabaseVersion { get; set; }

        [JsonProperty(PropertyName = "software_version")]
        public int SoftwareVersion { get; set; }

        [JsonProperty(PropertyName = "time_taken")]
        public int TimeTaken { get; set; }

        [JsonProperty(PropertyName = "random_number")]
        public int RandomNumber { get; set; }

        [JsonProperty(PropertyName = "time_second")]
        public int TimeSecond { get; set; }

        [JsonProperty(PropertyName = "time_minute")]
        public int TimeMinute { get; set; }

        [JsonProperty(PropertyName = "time_hour")]
        public int TimeHour { get; set; }

        [JsonProperty(PropertyName = "time_day_of_week")]
        public int TimeDayOfWeek { get; set; }

        [JsonProperty(PropertyName = "time_day")]
        public int TimeDay { get; set; }

        [JsonProperty(PropertyName = "time_month")]
        public int TimeMonth { get; set; }

        [JsonProperty(PropertyName = "time_year")]
        public int TimeYear { get; set; }

        public DateTime Time => new DateTime(TimeYear, TimeMonth, TimeDay, TimeHour, TimeMinute, TimeSecond);

        [JsonProperty(PropertyName = "filtered_input")]
        public string FilteredInput { get; set; }

        [JsonProperty(PropertyName = "clever_accuracy")]
        public int CleverAccuracy { get; set; }

        [JsonProperty(PropertyName = "clever_output")]
        public string CleverOutput { get; set; }
    }
}