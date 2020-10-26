using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using HtcSharp.HttpModule.Http.Abstractions;
using HtcSharp.HttpModule.Http.Abstractions.Extensions;
using Microsoft.EntityFrameworkCore;
using Nest;
using RestSharp;
using Where2Watch.Extensions;
using Where2Watch.Models;
using Where2Watch.Models.ElasticSearch;
using Where2Watch.Models.IMDb;
using Where2Watch.Mvc;

namespace Where2Watch.Controllers {
    public class DevController {

        public static List<string> DevKeys = new List<string> { "8McVgc89Hn" };

        [HttpGet("/api/dev/add/series")]
        public static async Task AddSeries(HttpContext httpContext) {
            if (!httpContext.Request.Query.TryGetValue("key", out var key) ||
                !httpContext.Request.Query.TryGetValue("t", out var t)) {
                await httpContext.Response.SendErrorAsync(1, "Invalid `dev key` or `t input`.");
                return;
            }
            if (key.Count == 0 || !DevKeys.Contains(key[0])) {
                await httpContext.Response.SendErrorAsync(2, "Invalid dev key.");
                return;
            }
            if (t.Count == 0) {
                await httpContext.Response.SendErrorAsync(3, "Invalid t input.");
                return;
            }

            var client = new RestClient($"https://imdb8.p.rapidapi.com/title/get-overview-details?currentCountry=US&tconst={t[0]}");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "imdb8.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", HtcPlugin.Config.RapidApi.Key);
            var response = await client.ExecuteAsync(request);
            var titleOverview = JsonSerializer.Deserialize<TitleOverviewDetails>(response.Content);

            client = new RestClient($"https://imdb8.p.rapidapi.com/title/get-seasons?tconst={t[0]}");
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

            await using var context = new DatabaseContext();
            var title = new Title {
                Id = Security.IdGen.GetId(),
                Type = TitleType.Series,
                OriginalName = titleOverview.Title.Title,
                Length = titleOverview.Title.RunningTimeInMinutes,
                IMDbId = t[0],
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

            await httpContext.Response.WriteAsync($"{{\"success\":true,\"data\":{response.Content}}}");
        }

        [HttpGet("/api/dev/add/platform")]
        public static async Task AddPlatform(HttpContext httpContext) {
            if (!httpContext.Request.Query.TryGetValue("key", out var key) ||
                !httpContext.Request.Query.TryGetValue("name", out var name) ||
                !httpContext.Request.Query.TryGetValue("link", out var link) ||
                !httpContext.Request.Query.TryGetValue("icon", out var icon)) {
                await httpContext.Response.SendErrorAsync(1, "Invalid `dev key` or `name` or `link` or `icon`.");
                return;
            }
            if (key.Count == 0 || !DevKeys.Contains(key[0])) {
                await httpContext.Response.SendErrorAsync(2, "Invalid dev key.");
                return;
            }
            if (name.Count == 0) {
                await httpContext.Response.SendErrorAsync(3, "Invalid name.");
                return;
            }
            if (link.Count == 0) {
                await httpContext.Response.SendErrorAsync(4, "Invalid link.");
                return;
            }
            if (icon.Count == 0) {
                await httpContext.Response.SendErrorAsync(5, "Invalid icon.");
                return;
            }

            await using var context = new DatabaseContext();

            var platform = new Platform {
                Id = Security.IdGen.GetId(),
                Name = name[0],
                Link = link[0],
                Icon = icon[0],
            };

            await context.Platforms.AddAsync(platform);
            await context.SaveChangesAsync();

            await httpContext.Response.WriteAsync($"{{\"success\":true}}");
        }

        /*
        [HttpGet("/api/dev/search/clear")]
        public static async Task ClearSearchIndex(HttpContext httpContext) {
            if (!httpContext.Request.Query.TryGetValue("key", out var key)) {
                await httpContext.Response.SendErrorAsync(1, "Invalid `dev key`.");
                return;
            }
            if (key.Count == 0 || !DevKeys.Contains(key[0])) {
                await httpContext.Response.SendErrorAsync(2, "Invalid dev key.");
                return;
            }

            HtcPlugin.TitleSearch.ClearIndex();
            await httpContext.Response.WriteAsync($"{{\"success\":true}}");
        }*/

        [HttpGet("/api/dev/search/fill")]
        public static async Task FillSearchIndex(HttpContext httpContext) {
            if (!httpContext.Request.Query.TryGetValue("key", out var key)) {
                await httpContext.Response.SendErrorAsync(1, "Invalid `dev key` or `t input`.");
                return;
            }
            if (key.Count == 0 || !DevKeys.Contains(key[0])) {
                await httpContext.Response.SendErrorAsync(2, "Invalid dev key.");
                return;
            }

            await using var context = new DatabaseContext();

            ElasticTitle[] titles = await (from t in context.Titles select new ElasticTitle(t)).ToArrayAsync();

            await HtcPlugin.ElasticClient.Indices.DeleteAsync("title", c => c);

            await HtcPlugin.ElasticClient.Indices.CreateAsync("title", c => c
                .Map(m => m
                    .AutoMap<ElasticTitle>()
                )
            );

            foreach (var title in titles) {
                //await HtcPlugin.ElasticClient.DeleteAsync(title, idx => idx.Index("title"));
                await HtcPlugin.ElasticClient.IndexAsync(title, idx => idx.Index("title"));
            }

            await httpContext.Response.WriteAsync($"{{\"success\":true}}");
        }

        [HttpGet("/api/dev/search/get")]
        public static async Task SearchIndex(HttpContext httpContext) {
            if (!httpContext.Request.Query.TryGetValue("key", out var key) ||
                !httpContext.Request.Query.TryGetValue("t", out var t)) {
                await httpContext.Response.SendErrorAsync(1, "Invalid `dev key`.");
                return;
            }
            if (key.Count == 0 || !DevKeys.Contains(key[0])) {
                await httpContext.Response.SendErrorAsync(2, "Invalid dev key.");
                return;
            }
            if (t.Count == 0) {
                await httpContext.Response.SendErrorAsync(3, "Invalid t input.");
                return;
            }

            string searchString = t[0].ToLower();


            ISearchResponse<ElasticTitle> response = await HtcPlugin.ElasticClient.SearchAsync<ElasticTitle>(s => s
                .Index("title")
                .From(0)
                .Size(5)
                .Query(q => q
                    .MatchPhrasePrefix(pp => pp
                        .Name("title_query")
                        .Boost(1.1)
                        .Field(f => f.Name)
                        .Query(searchString)
                    )
                )
            );

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new { success = true, search = searchString, result = response.Documents }));
        }
    }
}
