using Initial.Api.Models.Database.Tables;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Initial.Api.Models.Database
{
    public class User : Audit
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        public Guid PrivateId { get; set; }

        public Guid PublicId { get; set; }

        [MaxLength(250)]
        [Required]
        public string Email { get; set; }

        public Guid Password { get; set; }

        [ForeignKey("EnterpriseId")]
        public virtual int EnterpriseId { get; set; }

        public virtual Enterprise Enterprise { get; set; }
    }
}
