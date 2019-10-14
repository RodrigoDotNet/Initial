using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Initial.Api.Models
{
    public class AccountTicket
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Guid PublicId { get; set; }

        public Guid PrivateId { get; set; }
    }
}
