using Initial.Api.Models.Templates;
using System.ComponentModel.DataAnnotations;

namespace Initial.Api.Models
{
    public class EnterpriseResponse : EntityResponse
    {
        public int Id { get; set; }


        [Required]
        [StringLength(250, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
