using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Initial.Api.Models.Database
{
    public class Group : Templates.Audit
    {
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        [ForeignKey("Enterprise")]
        public virtual int? EnterpriseId { get; set; }

        public virtual Enterprise Enterprise { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }

        public virtual ICollection<AreaAccess> AreaAccess { get; set; }

        public virtual ICollection<PolicyAccess> PolicyAccess { get; set; }
    }
}
