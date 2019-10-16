using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Initial.Api.Models.Database
{
    public class Policy : Templates.Audit
    {
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<PolicyAccess> PolicyAccess { get; set; }

        [ForeignKey("Area")]
        public virtual int AreaId { get; set; }

        public virtual Area Area { get; set; }
    }
}
