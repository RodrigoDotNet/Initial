using System;
using System.ComponentModel.DataAnnotations;

namespace Initial.Api.Models.Database.Templates
{
    public abstract class Audit<TKey, TUserId>
        where TUserId : struct
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreationDate { get; set; }
            = DateTime.Now;

        public TUserId? CreationUserId { get; set; }

        public DateTime LastModifiedDate { get; set; }
            = DateTime.Now;

        public TUserId? LastModifiedUserId { get; set; }

        public bool Inactive { get; set; }
    }

    public abstract class Audit : Audit<int, int>
    {

    }

    public abstract class AuditInt64 : Audit<long, int>
    {

    }
}
