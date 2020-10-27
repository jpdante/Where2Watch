using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using HtcSharp.HttpModule.Http.Abstractions;
using HtcSharp.HttpModule.Http.Abstractions.Extensions;
using Where2Watch.Models;
using Where2Watch.Mvc;

namespace Where2Watch.Controllers {
    public class SharedController {

        public static string Countries;

        [HttpGet("/api/shared/country/get")]
        public static async Task GetCountries(HttpContext httpContext) {
            if (Countries == null) {
                var countries = new List<object>();
                foreach (Country country in Enum.GetValues(typeof(Country))) {
                    countries.Add(new { id = country.ToString(), name = country.GetCountryDescription(), num = (int)country });
                }
                Countries = JsonSerializer.Serialize(countries);
            }
            await httpContext.Response.WriteAsync(Countries);
        }

    }
}