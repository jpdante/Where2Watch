using Nest;
// ReSharper disable InconsistentNaming

namespace Where2Watch.Models.ElasticSearch {
    [ElasticsearchType(RelationName = "title")]
    public class ElasticTitle {

        public long Id { get; set; }

        [Keyword]
        public string IMDbId { get; set; }

        public string Name { get; set; }

        public ElasticTitle() { }

        public ElasticTitle(Title title) {
            Id = title.Id;
            IMDbId = title.IMDbId;
            Name = title.OriginalName;
        }

    }
}
