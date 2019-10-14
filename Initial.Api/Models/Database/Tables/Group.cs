using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Initial.Api.Models.Database
{
    public class Group : Templates.Audit
    {
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }

        public virtual ICollection<AreaAccess> AreaAccess { get; set; }

        public virtual ICollection<PolicyAccess> PolicyAccess { get; set; }
    }
}
