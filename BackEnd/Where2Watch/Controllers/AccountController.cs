using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using HtcSharp.Core.Logging.Abstractions;
using HtcSharp.HttpModule.Http.Abstractions;
using HtcSharp.HttpModule.Http.Abstractions.Extensions;
using Microsoft.EntityFrameworkCore;
using Where2Watch.Extensions;
using Where2Watch.Models.View;
using Where2Watch.Mvc;

namespace Where2Watch.Controllers {
    public class AccountController {
        [HttpGet("/api/account/get", true)]
        public static async Task GetAccount(HttpContext httpContext) {
            if (httpContext.Session.GetAccountId(out long accountId)) {
                await using var context = new DatabaseContext();
                var account = await (from a in context.Accounts where a.Id.Equals(accountId) select a).FirstOrDefaultAsync();
                if (account == null) {
                    await httpContext.Response.SendDecodeErrorAsync();
                    HtcPlugin.Logger.LogError($"[{Guid.NewGuid()}] [Route /api/account/get] Account should not be null! Account Id: {accountId}");
                    return;
                }

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new AccountView(account)));
            } else {
                await httpContext.Response.SendDecodeErrorAsync();
            }
        }
    }
}