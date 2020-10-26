using System.Text.Json;
using System.Threading.Tasks;
using HtcSharp.HttpModule.Http.Abstractions;
using HtcSharp.HttpModule.Http.Abstractions.Extensions;
using HtcSharp.HttpModule.Routing;
using Nest;
using Where2Watch.Models.ElasticSearch;
using Where2Watch.Models.Request;
using Where2Watch.Mvc;

namespace Where2Watch.Controllers {
    public class SearchController {

        [HttpPost("/api/search/get", ContentType.JSON)]
        public static async Task SearchIndex(HttpContext httpContext, GetSearch getSearch) {
            string searchString = getSearch.Data.ToLower();

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
