using System.ComponentModel.DataAnnotations;
// ReSharper disable InconsistentNaming

namespace Where2Watch.Models {
    public class Title {

        [Key]
        public long Id { get; set; }

        [Required]
        public TitleType Type { get; set; }

        [Required]
        public string OriginalName { get; set; }

        [Required]
        public int Length { get; set; }

        public string IMDbId { get; set; }

        public int Seasons { get; set; }

        public int Episodes { get; set; }

        public int Year { get; set; }

        public string Classification { get; set; }

        public string Genres { get; set; }

        public string Poster { get; set; }

        public string Outline { get; set; }

        public string Summary { get; set; }

        public uint Likes { get; set; }

        public uint Dislikes { get; set; }

    }
}
