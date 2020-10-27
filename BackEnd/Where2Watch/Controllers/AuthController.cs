using System;
using System.Linq;
using System.Threading.Tasks;
using HtcSharp.Core.Logging.Abstractions;
using HtcSharp.HttpModule.Http.Abstractions;
using HtcSharp.HttpModule.Http.Abstractions.Extensions;
using HtcSharp.HttpModule.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Where2Watch.Extensions;
using Where2Watch.Models;
using Where2Watch.Models.Request;
using Where2Watch.Models.View;
using Where2Watch.Mvc;
using Where2Watch.Security;

namespace Where2Watch.Controllers {
    public class AuthController {

        [HttpPost("/api/auth/register", ContentType.JSON)]
        public static async Task Register(HttpContext httpContext, RegisterData registerData) {

            var captchaResponse = await Captcha.ValidateCaptcha(registerData.Captcha, httpContext.Connection.RemoteIpAddress.ToString());
            if (!captchaResponse.Success) {
                await httpContext.Response.SendRequestErrorAsync(403, "Invalid captcha.", captchaResponse.ErrorMessage);
                return;
            }

            await using var context = new DatabaseContext();

            bool hasAccountWithEmail = await (from a in context.Accounts where a.Email.Equals(registerData.Email) select a).AnyAsync();
            if (hasAccountWithEmail) {
                await httpContext.Response.SendRequestErrorAsync(8, "An account with that email address already exists.");
                return;
            }

            bool hasAccountWithUsername = await (from a in context.Accounts where a.Username.Equals(registerData.Username) select a).AnyAsync();
            if (hasAccountWithUsername) {
                await httpContext.Response.SendRequestErrorAsync(9, "An account with that username already exists.");
            }

            string password = await Password.GeneratePassword(registerData.Password);

            var account = new Account {
                Id = Security.IdGen.GetId(),
                Guid = Guid.NewGuid().ToString("N"),
                Email = registerData.Email,
                Username = registerData.Username,
                Password = password,
                LastAccess = DateTime.UtcNow,
                AccountType = AccountType.Default,
            };
            try {
                await context.Accounts.AddAsync(account);
                await context.SaveChangesAsync();
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new { success = true }));
            } catch (Exception ex) {
                HtcPlugin.Logger.LogError(ex);
                await httpContext.Response.SendInternalErrorAsync(10, "An internal failure occurred. Please try again later.");
            }
        }

        [HttpPost("/api/auth/login", ContentType.JSON)]
        public static async Task Login(HttpContext httpContext, LoginData loginData) {

            var captchaResponse = await Captcha.ValidateCaptcha(loginData.Captcha, httpContext.Connection.RemoteIpAddress.ToString());
            if (!captchaResponse.Success) {
                await httpContext.Response.SendRequestErrorAsync(403, "Invalid captcha.", captchaResponse.ErrorMessage);
                return;
            }

            await using var context = new DatabaseContext();

            var account = await (from a in context.Accounts where a.Email.Equals(loginData.Email) select a).FirstOrDefaultAsync();
            if (account == null) {
                await httpContext.Response.SendRequestErrorAsync(4, "There is no account registered with this email.");
                return;
            }

            /*
            if (!account.Active) {
                await httpContext.Response.SendRequestErrorAsync(5, "You need to confirm your email before logging in.");
                return;
            }

            if (account.Banned) {
                var accountBan = await (from a in context.AccountBans where a.AccountId.Equals(account.Id) select a).FirstOrDefaultAsync();
                if (accountBan == null) {
                    await httpContext.Response.SendRequestErrorAsync(6, "This account is banned but it was not possible to obtain the reason, this may be a failure, please contact support.");
                    return;
                }
                await httpContext.Response.SendRequestErrorAsync(7, $"This account is banned. Ban ID: #{accountBan.Id}, Reason: {accountBan.Reason}", new {
                    id = accountBan.Id.ToString(),
                    reason = accountBan.Reason
                });
                return;
            }*/

            if (await Password.CheckPassword(account.Password, loginData.Password)) {
                try {
                    account.LastAccess = DateTime.UtcNow;
                    context.Accounts.Update(account);
                    await context.SaveChangesAsync();
                    httpContext.Session.Set("account", account.Id);
                    await httpContext.Session.CommitAsync();
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new { success = true, token = httpContext.Session.Id, account = new AccountView(account) }));
                } catch (Exception ex) {
                    HtcPlugin.Logger.LogError(ex);
                    await httpContext.Response.SendInternalErrorAsync(8, "An internal failure occurred while attempting to create the account. Please try again later.");
                }
            } else {
                await httpContext.Response.SendRequestErrorAsync(9, "The password is incorrect.");
            }
        }

        [HttpGet("/api/auth/checksession")]
        public static async Task CheckSession(HttpContext httpContext) {
            if (httpContext.Session.IsAvailable) {
                await httpContext.Response.WriteAsync("{\"isValid\":true}");
            } else {
                await httpContext.Response.WriteAsync("{\"isValid\":false}");
            }
        }

    }
}
