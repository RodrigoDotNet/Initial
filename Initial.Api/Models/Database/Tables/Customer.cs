using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Initial.Api.Models.Database
{
    public class Customer : Templates.Audit
    {
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        [MaxLength(250)]
        [Required]
        public string Email { get; set; }

        [ForeignKey("Enterprise")]
        public virtual int EnterpriseId { get; set; }

        public virtual Enterprise Enterprise { get; set; }
    }
}
