using System;

namespace Initial.Api.Models
{
    public class AccountTicket
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Guid PublicId { get; set; }

        public Guid PrivateId { get; set; }

        public int EnterpriseId { get; set; }
    }
}
