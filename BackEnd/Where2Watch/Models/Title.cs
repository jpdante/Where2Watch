using System.ComponentModel.DataAnnotations;

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

        public int Seasons { get; set; }

        public int Episodes { get; set; }

        public string Poster { get; set; }

        public string Summary { get; set; }

        public uint Likes { get; set; }

        public uint Dislikes { get; set; }

    }
}
