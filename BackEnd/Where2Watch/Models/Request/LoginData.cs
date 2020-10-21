using System.Threading.Tasks;
using HtcSharp.HttpModule.Http.Abstractions;
using Where2Watch.Extensions;
using Where2Watch.Mvc;

namespace Where2Watch.Models.Request {
    public class LoginData : IHttpJsonObject {

        public string Email { get; set; }

        public string Password { get; set; }

        public string Captcha { get; set; }

        public async Task<bool> ValidateData(HttpContext httpContext) {
            if (!Email.ValidateEmail()) {
                await httpContext.Response.SendRequestErrorAsync(1, "The e-mail is invalid or empty.");
                return false;
            }
            if (!Password.ValidatePasswordLength()) {
                await httpContext.Response.SendRequestErrorAsync(2, "The password is invalid or too small.");
                return false;
            }
            if (string.IsNullOrEmpty(Captcha)) {
                await httpContext.Response.SendRequestErrorAsync(3, "The captcha is invalid or empty.");
                return false;
            }
            return true;
        }
    }
}
