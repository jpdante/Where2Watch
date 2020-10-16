using System.ComponentModel.DataAnnotations;

namespace Where2Watch.Models {
    public class Platform {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }

        [Required]
        public string Link { get; set; }
    }
}