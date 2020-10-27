using System;
using System.Threading.Tasks;
using HtcSharp.Core.Logging.Abstractions;
using HtcSharp.HttpModule.Http.Abstractions;
using Where2Watch.Extensions;
using Where2Watch.Mvc;

namespace Where2Watch.Models.Request {
    public class GetSearch : IHttpJsonObject {

        public string Data { get; set; }

        public async Task<bool> ValidateData(HttpContext httpContext) {
            if (string.IsNullOrEmpty(Data)) {
                await httpContext.Response.SendRequestErrorAsync(1, "Data is invalid or empty.");
                return false;
            }
            if (Data.Length > 64) {
                await httpContext.Response.SendRequestErrorAsync(1, "Data cannot have more than 64 characters.");
                return false;
            }
            return true;
        }
    }
}
