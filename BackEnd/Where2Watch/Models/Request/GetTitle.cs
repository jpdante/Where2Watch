using System;
using System.Threading.Tasks;
using HtcSharp.Core.Logging.Abstractions;
using HtcSharp.HttpModule.Http.Abstractions;
using Newtonsoft.Json;
using Where2Watch.Extensions;
using Where2Watch.Mvc;

namespace Where2Watch.Models.Request {
    public class GetTitle : IHttpJsonObject {

        public string Id { get; set; }

        public string Country { get; set; }

        [JsonIgnore]
        public Country CountryData;

        public async Task<bool> ValidateData(HttpContext httpContext) {
            if (string.IsNullOrEmpty(Id)) {
                await httpContext.Response.SendRequestErrorAsync(1, "Id is invalid or empty.");
                return false;
            }
            HtcPlugin.Logger.LogDebug(Country);
            if (string.IsNullOrEmpty(Country)) {
                await httpContext.Response.SendRequestErrorAsync(2, "Country is invalid or empty.");
                return false;
            }
            if (!Enum.TryParse(Country, true, out CountryData)) {
                await httpContext.Response.SendRequestErrorAsync(3, "Failed to parse country.");
                return false;
            }
            return true;
        }
    }
}
