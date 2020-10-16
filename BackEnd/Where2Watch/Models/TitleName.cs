﻿using System.ComponentModel.DataAnnotations;

namespace Where2Watch.Models {
    public class TitleName {

        [Key]
        public long Id { get; set; }

        [Required]
        public long TitleId { get; set; }
        public Title Title { get; set; }

        [Required]
        [MaxLength(2)]
        public string Country { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
