using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Where2Watch.Models {
    public class Account {

        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Guid { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(32)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public AccountType AccountType { get; set; }

        [Required]
        [Column(TypeName = "TIMESTAMP")]
        public DateTime LastAccess { get; set; }
    }
}
