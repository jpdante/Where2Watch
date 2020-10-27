using System;
using System.Threading.Tasks;
using HtcSharp.HttpModule.Http.Abstractions;
using Newtonsoft.Json;
using Where2Watch.Extensions;
using Where2Watch.Mvc;

namespace Where2Watch.Models.Request {
    public class GetAvailability : IHttpJsonObject {

        public string Id { get; set; }

        public string Country { get; set; }

        [JsonIgnore]
        public Country CountryData;

        [JsonIgnore]
        public long IdData;

        public async Task<bool> ValidateData(HttpContext httpContext) {
            if (string.IsNullOrEmpty(Id)) {
                await httpContext.Response.SendRequestErrorAsync(1, "Id is invalid or empty.");
                return false;
            }
            if (string.IsNullOrEmpty(Country)) {
                await httpContext.Response.SendRequestErrorAsync(1, "Country is invalid or empty.");
                return false;
            }
            if (!long.TryParse(Id, out IdData)) {
                await httpContext.Response.SendRequestErrorAsync(1, "Failed to parse id.");
                return false;
            }
            if (!Enum.TryParse(Country, true, out CountryData)) {
                await httpContext.Response.SendRequestErrorAsync(1, "Failed to parse country.");
                return false;
            }
            return true;
        }
    }
}