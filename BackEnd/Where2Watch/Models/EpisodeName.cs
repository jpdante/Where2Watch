using System.ComponentModel.DataAnnotations;

namespace Where2Watch.Models {
    public class EpisodeName {
        [Key]
        public long Id { get; set; }

        [Required]
        public long EpisodeId { get; set; }
        public Episode Episode { get; set; }

        [Required]
        public Country Country { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}