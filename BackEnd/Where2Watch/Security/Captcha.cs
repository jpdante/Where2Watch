using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using HtcSharp.Core.Logging.Abstractions;
using Newtonsoft.Json;
using Where2Watch.Models;

namespace Where2Watch.Security {
    public class Captcha {

        private static string _secreteKey;

        public Captcha(string secreteKey) {
            string variable = Environment.GetEnvironmentVariable("DevMode");
            bool isDev = variable != null && variable.Equals("true");
            _secreteKey = isDev ? "0x0000000000000000000000000000000000000000" : secreteKey;
        }

        public static async Task<CaptchaResponse> ValidateCaptcha(string captchaToken, string ip) {
            try {
                var client = new WebClient { Headers = { [HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded" } };
                string postData = $"secret={_secreteKey}&response={captchaToken}&remoteip={ip}";
                //HtcPlugin.Logger.LogDebug(postData);
                string result = await client.UploadStringTaskAsync("https://hcaptcha.com/siteverify", postData);
                //HtcPlugin.Logger.LogDebug(result);
                return JsonConvert.DeserializeObject<CaptchaResponse>(result);
            } catch(Exception ex) {
                HtcPlugin.Logger.LogError(ex);
                return new CaptchaResponse() {
                    Success = false,
                    ErrorMessage = new List<string> {
                        "Internal decode error."
                    }
                };
            }
        }

    }
}