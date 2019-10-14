using System.ComponentModel.DataAnnotations.Schema;

namespace Initial.Api.Models.Database
{
    public class AreaAccess : Templates.Audit
    {
        [ForeignKey("Group")]
        public virtual int GroupId { get; set; }

        public virtual Group Group { get; set; }

        [ForeignKey("Area")]
        public virtual int AreaId { get; set; }

        public virtual Area Area { get; set; }

        public bool CanRead { get; set; }

        public bool CanCreate { get; set; }

        public bool CanModify { get; set; }

        public bool CanDelete { get; set; }
    }
}
