using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using HtcSharp.HttpModule.Http.Abstractions;
using HtcSharp.HttpModule.Http.Abstractions.Extensions;
using HtcSharp.HttpModule.Routing;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using Where2Watch.Extensions;
using Where2Watch.Models;
using Where2Watch.Models.ElasticSearch;
using Where2Watch.Models.IMDb;
using Where2Watch.Models.Request;
using Where2Watch.Models.View;
using Where2Watch.Mvc;

namespace Where2Watch.Controllers {
    public class AdminController {
        [HttpGet("/api/admin/platform/get", true)]
        public static async Task GetPlatforms(HttpContext httpContext) {
            if (!httpContext.Session.GetAccountId(out long accountId)) throw new HttpException(500, "Failed to get AccountId");

            await using var context = new DatabaseContext();
            var account = await (from a in context.Accounts where a.Id.Equals(accountId) select a).FirstOrDefaultAsync();
            if (account == null) throw new HttpException(500, "Account does not exist");

            if (account.AccountType != AccountType.Admin) throw new HttpException(403, "Access denied");

            PlatformView[] platforms = await (from p in context.Platforms select new PlatformView(p)).ToArrayAsync();

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(platforms));
        }

        [HttpPost("/api/admin/series/add", ContentType.JSON, true)]
        public static async Task AddSeries(HttpContext httpContext, AddSeries addSeries) {
            if (!httpContext.Session.GetAccountId(out long accountId)) throw new HttpException(500, "Failed to get AccountId");

            await using var context = new DatabaseContext();
            var account = await (from a in context.Accounts where a.Id.Equals(accountId) select a).FirstOrDefaultAsync();
            if (account == null) throw new HttpException(500, "Account does not exist");

            if (account.AccountType != AccountType.Admin) throw new HttpException(403, "Access denied");

            var hasAnyTitle = await (from t in context.Titles where t.IMDbId.Equals(addSeries.ImdbId) select t).AnyAsync();
            if (hasAnyTitle) throw new HttpException(500, "There is already a title with that id.");

            var client = new RestClient($"https://imdb8.p.rapidapi.com/title/get-overview-details?currentCountry=US&tconst={addSeries.ImdbId}");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "imdb8.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", HtcPlugin.Config.RapidApi.Key);
            var response = await client.ExecuteAsync(request);
            var titleOverview = JsonSerializer.Deserialize<TitleOverviewDetails>(response.Content);

            client = new RestClient($"https://imdb8.p.rapidapi.com/title/get-seasons?tconst={addSeries.ImdbId}");
            request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "imdb8.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", HtcPlugin.Config.RapidApi.Key);
            response = await client.ExecuteAsync(request);
            SeasonDetails[] seasonsDetails = JsonSerializer.Deserialize<SeasonDetails[]>(response.Content);

            string outline = titleOverview.PlotOutline?.Text;
            string summary = titleOverview.PlotSummary?.Text;

            string classification = null;
            try {
                classification = titleOverview.Certificates.Us[0].Certificate;
            } catch {
                // ignored
            }

            var title = new Title {
                Id = Security.IdGen.GetId(),
                Type = TitleType.Series,
                OriginalName = titleOverview.Title.Title,
                Length = titleOverview.Title.RunningTimeInMinutes,
                IMDbId = addSeries.ImdbId,
                Seasons = 0,
                Episodes = titleOverview.Title.NumberOfEpisodes,
                Year = titleOverview.Title.Year,
                Poster = titleOverview.Title.Image.Url,
                Genres = JsonSerializer.Serialize(titleOverview.Genres, typeof(string[])),
                Classification = classification,
                Summary = summary,
                Outline = outline,
                Likes = 0,
                Dislikes = 0,
            };
            await context.Titles.AddAsync(title);

            var episodes = 0;
            var seasons = 0;

            foreach (var season in seasonsDetails) {
                var seasonModel = new Season {
                    Id = Security.IdGen.GetId(),
                    Name = null,
                    Number = season.Season,
                    TitleId = title.Id,
                };
                await context.Seasons.AddAsync(seasonModel);
                seasons++;
                foreach (var episode in season.Episodes) {
                    var episodeModel = new Episode {
                        Id = Security.IdGen.GetId(),
                        OriginalName = episode.Title,
                        Number = episode.Episode,
                        Year = episode.Year,
                        IMDbId = episode.Id,
                        SeasonId = seasonModel.Id,
                        TitleId = title.Id,
                    };
                    await context.Episodes.AddAsync(episodeModel);
                    episodes++;
                }
            }

            await context.SaveChangesAsync();

            title.Episodes = episodes;
            title.Seasons = seasons;
            context.Titles.Update(title);

            await context.SaveChangesAsync();

            await HtcPlugin.ElasticClient.IndexAsync(new ElasticTitle(title), idx => idx.Index("title"));

            await httpContext.Response.WriteAsync($"{{\"success\":true,\"data\":{response.Content}}}");
        }

        [HttpPost("/api/admin/platform/add", ContentType.JSON, true)]
        public static async Task AddPlatform(HttpContext httpContext, AddPlatform addPlatform) {
            if (!httpContext.Session.GetAccountId(out long accountId)) throw new HttpException(500, "Failed to get AccountId");

            await using var context = new DatabaseContext();
            var account = await (from a in context.Accounts where a.Id.Equals(accountId) select a).FirstOrDefaultAsync();
            if (account == null) throw new HttpException(500, "Account does not exist");

            if (account.AccountType != AccountType.Admin) throw new HttpException(403, "Access denied");

            var platform = new Platform {
                Id = Security.IdGen.GetId(),
                Name = addPlatform.Name,
                Link = addPlatform.Link,
                Icon = addPlatform.Icon,
                Country = addPlatform.CountryData
            };

            await context.Platforms.AddAsync(platform);
            await context.SaveChangesAsync();

            await httpContext.Response.WriteAsync($"{{\"success\":true}}");
        }

        [HttpPost("/api/admin/availability/add", ContentType.JSON, true)]
        public static async Task AddAvailability(HttpContext httpContext, AddAvailability addAvailability) {
            if (!httpContext.Session.GetAccountId(out long accountId)) throw new HttpException(500, "Failed to get AccountId");

            await using var context = new DatabaseContext();
            var account = await (from a in context.Accounts where a.Id.Equals(accountId) select a).FirstOrDefaultAsync();
            if (account == null) throw new HttpException(500, "Account does not exist");

            if (account.AccountType != AccountType.Admin) throw new HttpException(403, "Access denied");

            var title = await (from a in context.Titles where a.IMDbId.Equals(addAvailability.ImdbId) select a).FirstOrDefaultAsync();
            if (title == null) {
                await httpContext.Response.SendRequestErrorAsync(404, "Title not found");
                return;
            }

            var platform = await (from p in context.Platforms where p.Id.Equals(addAvailability.PlatformId) select p).FirstOrDefaultAsync();
            if (platform == null) {
                await httpContext.Response.SendRequestErrorAsync(404, "Platform not found");
                return;
            }

            var availability = new TitleAvailability {
                Id = Security.IdGen.GetId(),
                TitleId = title.Id,
                PlatformId = platform.Id,
                Link = addAvailability.Link,
                Country = addAvailability.CountryData
            };

            await context.TitleAvailabilities.AddAsync(availability);
            await context.SaveChangesAsync();

            await httpContext.Response.WriteAsync($"{{\"success\":true}}");
        }
    }
}