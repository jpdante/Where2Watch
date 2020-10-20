using System.ComponentModel.DataAnnotations;

namespace Where2Watch.Models {
    public class TitleAvailability {
        [Key]
        public long Id { get; set; }

        [Required]
        public long TitleId { get; set; }
        public Title Title { get; set; }

        [Required]
        public long PlatformId { get; set; }
        public Platform Platform { get; set; }

        [Required]
        public Country Country { get; set; }

        public string Link { get; set; }
    }
}
