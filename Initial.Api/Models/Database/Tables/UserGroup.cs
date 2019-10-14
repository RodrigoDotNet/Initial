using System.ComponentModel.DataAnnotations.Schema;

namespace Initial.Api.Models.Database
{
    public class UserGroup : Templates.Audit
    {
        [ForeignKey("Group")]
        public virtual int GroupId { get; set; }

        public virtual Group Group { get; set; }

        [ForeignKey("User")]
        public virtual int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
