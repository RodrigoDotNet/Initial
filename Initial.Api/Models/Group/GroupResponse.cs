using Initial.Api.Models.Templates;
using System.ComponentModel.DataAnnotations;

namespace Initial.Api.Models
{
    public class GroupResponse : EntityResponse
    {
        /// <summary>
        /// Código
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        [StringLength(250, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
