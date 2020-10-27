using System;
using System.Threading.Tasks;
using HtcSharp.HttpModule.Http.Abstractions;
using Newtonsoft.Json;
using Where2Watch.Extensions;
using Where2Watch.Mvc;

namespace Where2Watch.Models.Request {
    public class AddAvailability : IHttpJsonObject {

        public string ImdbId { get; set; }
        public string Country { get; set; }
        public string Platform { get; set; }
        public string Link { get; set; }

        [JsonIgnore]
        public Country CountryData;

        [JsonIgnore]
        public long PlatformId;

        public async Task<bool> ValidateData(HttpContext httpContext) {
            if (string.IsNullOrEmpty(ImdbId)) {
                await httpContext.Response.SendRequestErrorAsync(1, "ImdbId is invalid or empty.");
                return false;
            }
            if (string.IsNullOrEmpty(Country)) {
                await httpContext.Response.SendRequestErrorAsync(1, "Country is invalid or empty.");
                return false;
            }
            if (string.IsNullOrEmpty(Platform)) {
                await httpContext.Response.SendRequestErrorAsync(1, "Platform is invalid or empty.");
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
            if (!long.TryParse(Platform, out PlatformId)) {
                await httpContext.Response.SendRequestErrorAsync(3, "Failed to parse platform id.");
                return false;
            }
            return true;
        }
    }
}
