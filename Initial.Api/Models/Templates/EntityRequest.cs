using System;

namespace Initial.Api.Models.Templates
{
    public abstract class EntityRequest
    {
        public DateTime? EntityVersion { get; set; }
    }
}
