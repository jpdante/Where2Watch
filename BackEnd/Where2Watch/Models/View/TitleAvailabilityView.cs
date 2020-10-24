using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Where2Watch.Models.View {
    public class TitleAvailabilityView {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("platform")]
        public PlatformView Platform { get; set; }

        public TitleAvailabilityView(TitleAvailability titleAvailability, PlatformView platform) {
            Id = titleAvailability.Id.ToString();
            Country = titleAvailability.Country.ToString();
            Link = titleAvailability.Link;
            Platform = platform;
        }
    }
}
