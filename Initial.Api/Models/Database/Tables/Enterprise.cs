using Initial.Api.Models.Database.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Initial.Api.Models.Database
{
    public class Enterprise : Audit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        public Guid PrivateId { get; set; }

        public Guid PublicId { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
