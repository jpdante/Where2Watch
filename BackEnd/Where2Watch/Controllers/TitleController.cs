using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using HtcSharp.HttpModule.Http.Abstractions;
using HtcSharp.HttpModule.Http.Abstractions.Extensions;
using HtcSharp.HttpModule.Routing;
using Microsoft.EntityFrameworkCore;
using Where2Watch.Extensions;
using Where2Watch.Models;
using Where2Watch.Models.Request;
using Where2Watch.Models.View;
using Where2Watch.Mvc;

namespace Where2Watch.Controllers {
    public class TitleController {

        [HttpPost("/api/title/get", ContentType.JSON)]
        public static async Task GetTitle(HttpContext httpContext, GetTitle getTitle) {
            await using var context = new DatabaseContext();

            var title = await (from a in context.Titles where a.IMDbId.Equals(getTitle.Id) select a).FirstOrDefaultAsync();
            if (title == null) {
                await httpContext.Response.SendRequestErrorAsync(404, "Title not found");
                return;
            }

            TitleAvailabilityView[] titleAvailabilities = await (
                from ta in context.TitleAvailabilities
                join p in context.Platforms on ta.PlatformId equals p.Id
                where ta.Country.Equals(getTitle.CountryData)
                select new TitleAvailabilityView(ta, new PlatformView(p))).ToArrayAsync();

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new TitleView(title, titleAvailabilities)));
        }
    }
}
