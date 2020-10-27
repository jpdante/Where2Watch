using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using HtcSharp.HttpModule.Http.Abstractions;
using HtcSharp.HttpModule.Http.Abstractions.Extensions;
using Microsoft.EntityFrameworkCore;
using Where2Watch.Models.View;
using Where2Watch.Mvc;

namespace Where2Watch.Controllers {
    public class FeedController {

        [HttpGet("/api/feed/get")]
        public static async Task GetFeed(HttpContext httpContext) {
            await using var context = new DatabaseContext();

            TitleView[] latest = await (from t in context.Titles orderby t.Id descending select new TitleView(t, null)).Take(6).ToArrayAsync();

            TitleView[] mostViewed = await (from t in context.Titles orderby t.Id select new TitleView(t, null)).Take(6).ToArrayAsync();

            TitleView[] mostLiked = await (from t in context.Titles orderby t.Likes descending select new TitleView(t, null)).Take(6).ToArrayAsync();

            await httpContext.Response.WriteAsync( JsonSerializer.Serialize(new { latest, mostViewed, mostLiked }));
        }
    }
}