﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Initial.Api.Models.Database
{
    public class User : Templates.Audit
    {
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        public Guid PrivateId { get; set; }

        public Guid PublicId { get; set; }

        [MaxLength(250)]
        [Required]
        public string Email { get; set; }

        public Guid Password { get; set; }

        [ForeignKey("Enterprise")]
        public virtual int EnterpriseId { get; set; }

        public virtual Enterprise Enterprise { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
