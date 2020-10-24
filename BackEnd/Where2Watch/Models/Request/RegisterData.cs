using System.Threading.Tasks;
using HtcSharp.HttpModule.Http.Abstractions;
using Where2Watch.Extensions;
using Where2Watch.Mvc;

namespace Where2Watch.Models.Request {
    public class RegisterData : IHttpJsonObject {

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Captcha { get; set; }

        public string Language { get; set; }

        public async Task<bool> ValidateData(HttpContext httpContext) {
            if (!Email.ValidateEmail()) {
                await httpContext.Response.SendRequestErrorAsync(1, "The e-mail is invalid or empty.");
                return false;
            }
            if (!Username.ValidateUsername()) {
                await httpContext.Response.SendRequestErrorAsync(2, "The username is invalid or too long.");
                return false;
            }
            if (!Password.ValidatePasswordLength()) {
                await httpContext.Response.SendRequestErrorAsync(3, "The password is invalid or too small.");
                return false;
            }
            if (!ConfirmPassword.Equals(Password)) {
                await httpContext.Response.SendRequestErrorAsync(4, "The password confirmation is not the same as password.");
                return false;
            }
            if (string.IsNullOrEmpty(Captcha)) {
                await httpContext.Response.SendRequestErrorAsync(6, "The captcha is invalid or empty.");
                return false;
            }
            if (!Language.ValidateLanguage()) {
                await httpContext.Response.SendRequestErrorAsync(7, "The language is invalid or empty.");
                return false;
            }
            return true;
        }
    }
}