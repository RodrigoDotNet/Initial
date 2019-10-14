using System.ComponentModel.DataAnnotations;

namespace Initial.Api.Models
{
    public class EnterpriseRequest
    {
        [Required]
        [StringLength(250, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
