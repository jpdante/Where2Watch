// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Where2Watch.Models.View {
    public class TitleView {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("originalName")]
        public string OriginalName { get; set; }

        [JsonPropertyName("length")]
        public int Length { get; set; }

        [JsonPropertyName("imdbId")]
        public string IMDbId { get; set; }

        [JsonPropertyName("seasons")]
        public int Seasons { get; set; }

        [JsonPropertyName("episodes")]
        public int Episodes { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("classification")]
        public string Classification { get; set; }

        [JsonPropertyName("genres")]
        public string[] Genres { get; set; }

        [JsonPropertyName("poster")]
        public string Poster { get; set; }

        [JsonPropertyName("outline")]
        public string Outline { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("likes")]
        public int Likes { get; set; }

        [JsonPropertyName("dislikes")]
        public int Dislikes { get; set; }

        [JsonPropertyName("availability")]
        public TitleAvailabilityView[] Availability { get; set; }

        public TitleView(Title title, TitleAvailabilityView[] availability) {
            Id = title.Id.ToString();
            OriginalName = title.OriginalName;
            Length = title.Length;
            IMDbId = title.IMDbId;
            Seasons = title.Seasons;
            Episodes = title.Episodes;
            Year = title.Year;
            Classification = title.Classification;
            Genres = JsonSerializer.Deserialize<string[]>(title.Genres);
            Poster = title.Poster;
            Outline = title.Outline;
            Summary = title.Summary;
            Likes = (int) title.Likes;
            Dislikes = (int) title.Dislikes;
            Availability = availability;
        }
    }
}