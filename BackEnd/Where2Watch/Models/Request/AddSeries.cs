using System.Threading.Tasks;
using HtcSharp.HttpModule.Http.Abstractions;
using Where2Watch.Extensions;
using Where2Watch.Mvc;

namespace Where2Watch.Models.Request {
    public class AddSeries : IHttpJsonObject {

        public string ImdbId { get; set; }

        public async Task<bool> ValidateData(HttpContext httpContext) {
            if (string.IsNullOrEmpty(ImdbId)) {
                await httpContext.Response.SendRequestErrorAsync(1, "ImdbId is invalid or empty.");
                return false;
            }
            return true;
        }
    }
}
