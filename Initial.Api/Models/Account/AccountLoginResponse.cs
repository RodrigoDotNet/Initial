using System;

namespace Initial.Api.Models
{
    public class AccountLoginResponse
    {
        /// <summary>
        /// Código
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Chave pública
        /// </summary>
        public Guid PublicId { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Token (JWT)
        /// </summary>
        public string Token { get; set; }
    }
}
