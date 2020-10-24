using System.ComponentModel.DataAnnotations;
// ReSharper disable InconsistentNaming

namespace Where2Watch.Models {
    public class Episode {
        [Key]
        public long Id { get; set; }

        [Required]
        public long TitleId { get; set; }
        public Title Title { get; set; }

        [Required]
        public long SeasonId { get; set; }
        public Season Season { get; set; }

        [Required]
        public int Number { get; set; }

        public int Year { get; set; }

        public string IMDbId { get; set; }

        [Required]
        [MaxLength(256)]
        public string OriginalName { get; set; }
    }
}