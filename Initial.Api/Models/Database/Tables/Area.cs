using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Initial.Api.Models.Database
{
    public class Area : Templates.Audit
    {
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<AreaAccess> AreaAccess { get; set; }

        public virtual ICollection<Policy> Policies { get; set; }
    }
}
