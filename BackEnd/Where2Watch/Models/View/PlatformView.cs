using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Where2Watch.Models.View {
    public class PlatformView {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        public PlatformView(Platform platform) {
            Id = platform.Id.ToString();
            Name = platform.Name;
            Icon = platform.Icon;
            Link = platform.Link;
            Country = platform.Country.ToString();
        }
    }
}
