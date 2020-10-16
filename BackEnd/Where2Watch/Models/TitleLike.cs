using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Where2Watch.Models {
    public class TitleLike {
        [Required]
        public long AccountId { get; set; }
        public Account Account { get; set; }

        [Required]
        public long TitleId { get; set; }
        public Title Title { get; set; }

        [DefaultValue(null)]
        public bool? IsLike { get; set; }

        [Required]
        [Column(TypeName = "TIMESTAMP")]
        public DateTime LastUpdate { get; set; }
    }
}