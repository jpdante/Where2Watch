using System.ComponentModel.DataAnnotations;

namespace Where2Watch.Models {
    public class Platform {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        public Country Country { get; set; }
    }
}