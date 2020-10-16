using System.ComponentModel.DataAnnotations;

namespace Where2Watch.Models {
    public class Season {
        [Key]
        public long Id { get; set; }

        [Required]
        public long TitleId { get; set; }
        public Title Title { get; set; }

        [Required]
        public int Number { get; set; }

        public string Name { get; set; }
    }
}