using System.ComponentModel.DataAnnotations;

namespace Where2Watch.Models {
    public class EpisodeName {
        [Key]
        public long Id { get; set; }

        [Required]
        public long EpisodeId { get; set; }
        public Episode Episode { get; set; }

        [Required]
        [MaxLength(2)]
        public string Country { get; set; }

        [Required]
        public string Name { get; set; }
    }
}