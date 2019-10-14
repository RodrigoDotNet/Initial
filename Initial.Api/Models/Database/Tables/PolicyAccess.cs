using System.ComponentModel.DataAnnotations.Schema;

namespace Initial.Api.Models.Database
{
    public class PolicyAccess : Templates.Audit
    {
        [ForeignKey("Group")]
        public virtual int GroupId { get; set; }

        public virtual Group Group { get; set; }

        [ForeignKey("Policy")]
        public virtual int PolicyId { get; set; }

        public virtual Policy Policy { get; set; }
    }
}
