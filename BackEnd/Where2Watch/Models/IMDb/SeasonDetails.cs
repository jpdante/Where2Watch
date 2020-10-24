using System.Text.Json.Serialization;

namespace Where2Watch.Models.IMDb {
    public class SeasonDetails {

        [JsonPropertyName("season")]
        public int Season { get; set; }

        [JsonPropertyName("episodes")]
        public EpisodeDetails[] Episodes { get; set; }

        public class EpisodeDetails {

            [JsonPropertyName("episode")]
            public int Episode { get; set; }

            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("season")]
            public int Season { get; set; }

            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("titleType")]
            public string TitleType { get; set; }

            [JsonPropertyName("year")]
            public int Year { get; set; }

        }
    }
}
