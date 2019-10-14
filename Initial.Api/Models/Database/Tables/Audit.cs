using System;

namespace Initial.Api.Models.Database.Tables
{
    public abstract class Audit
    {
        public DateTime CreationDate { get; set; }
            = DateTime.Now;

        public int? CreationUserId { get; set; }

        public DateTime LastModifiedDate { get; set; }
            = DateTime.Now;

        public int? LastModifiedUserId { get; set; }

        public bool Deleted { get; set; }
    }
}
