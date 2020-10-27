using System;
using System.Threading.Tasks;
using HtcSharp.HttpModule.Http.Abstractions;
using Newtonsoft.Json;
using Where2Watch.Extensions;
using Where2Watch.Mvc;

namespace Where2Watch.Models.Request {
    public class AddPlatform : IHttpJsonObject {

        public string Name { get; set; }
        public string Country { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }

        [JsonIgnore]
        public Country CountryData;

        public async Task<bool> ValidateData(HttpContext httpContext) {
            if (string.IsNullOrEmpty(Icon)) {
                await httpContext.Response.SendRequestErrorAsync(1, "Icon is invalid or empty.");
                return false;
            }
            if (string.IsNullOrEmpty(Country)) {
                await httpContext.Response.SendRequestErrorAsync(1, "Country is invalid or empty.");
                return false;
            }
            if (string.IsNullOrEmpty(Name)) {
                await httpContext.Response.SendRequestErrorAsync(1, "Name is invalid or empty.");
                return false;
            }
            if (string.IsNullOrEmpty(Link)) {
                await httpContext.Response.SendRequestErrorAsync(1, "Link is invalid or empty.");
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
