using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codenation.Challenge.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("id"), Required]
        public int Id { get; set; }

        [Column("full_name"), Required]
        [StringLength(100)]
        public string Full_Name { get; set; }

        [Column("email"), Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Column("nickname"), Required]
        [StringLength(50)]
        public string NickName { get; set; }

        [Column("password"), Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Column("created_at")]
        [Required]
        public DateTime Created_At { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }

    }
}
