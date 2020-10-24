using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using HtcSharp.HttpModule.Http.Abstractions;
using HtcSharp.HttpModule.Http.Abstractions.Extensions;
using Microsoft.EntityFrameworkCore;
using Where2Watch.Models.View;
using Where2Watch.Mvc;

namespace Where2Watch.Controllers {
    public class PlatformController {
        [HttpGet("/api/platform/get")]
        public static async Task GetTitle(HttpContext httpContext) {
            await using var context = new DatabaseContext();

            PlatformView[] platforms = await (from p in context.Platforms select new PlatformView(p)).ToArrayAsync();

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(platforms));
        }
    }
}