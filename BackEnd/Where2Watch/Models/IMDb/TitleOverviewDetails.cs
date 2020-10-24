using System.Text.Json.Serialization;

namespace Where2Watch.Models.IMDb {
    public class TitleOverviewDetails {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public TitleDetails Title { get; set; }

        [JsonPropertyName("genres")]
        public string[] Genres { get; set; }

        [JsonPropertyName("certificates")]
        public CertificatesDetails Certificates { get; set; }

        [JsonPropertyName("plotOutline")]
        public PlotOutlineDetails PlotOutline { get; set; }

        [JsonPropertyName("plotSummary")]
        public PlotSummaryDetails PlotSummary { get; set; }

        public class TitleDetails {

            [JsonPropertyName("image")]
            public ImageDetails Image { get; set; }

            [JsonPropertyName("runningTimeInMinutes")]
            public int RunningTimeInMinutes { get; set; }

            [JsonPropertyName("nextEpisode")]
            public string NextEpisode { get; set; }

            [JsonPropertyName("numberOfEpisodes")]
            public int NumberOfEpisodes { get; set; }

            [JsonPropertyName("seriesEndYear")]
            public int SeriesEndYear { get; set; }

            [JsonPropertyName("seriesStartYear")]
            public int SeriesStartYear { get; set; }

            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("titleType")]
            public string TitleType { get; set; }

            [JsonPropertyName("year")]
            public int Year { get; set; }

        }

        public class CertificatesDetails {

            [JsonPropertyName("US")]
            public CertificateDetails[] Us { get; set; }

        }

        public class CertificateDetails {

            [JsonPropertyName("certificate")]
            public string Certificate { get; set; }

            [JsonPropertyName("country")]
            public string Country { get; set; }

        }

        public class ImageDetails {

            [JsonPropertyName("height")]
            public int Height { get; set; }

            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("width")]
            public int Width { get; set; }

        }

        public class PlotOutlineDetails {

            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("text")]
            public string Text { get; set; }

        }

        public class PlotSummaryDetails {

            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("text")]
            public string Text { get; set; }

        }
    }
}
