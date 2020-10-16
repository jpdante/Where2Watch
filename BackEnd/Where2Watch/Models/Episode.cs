using System.ComponentModel.DataAnnotations;

namespace Where2Watch.Models {
    public class Episode {
        [Key]
        public long Id { get; set; }

        [Required]
        public long TitleId { get; set; }
        public Title Title { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public string OriginalName { get; set; }
    }
}