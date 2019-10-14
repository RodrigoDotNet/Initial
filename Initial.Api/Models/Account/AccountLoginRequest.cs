using System.ComponentModel.DataAnnotations;

namespace Initial.Api.Models
{
    public class AccountLoginRequest
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [StringLength(250, MinimumLength = 6)]
        public string Email { get; set; }

        /// <summary>
        /// Senha
        /// </summary>
        [Required]
        [StringLength(18, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
