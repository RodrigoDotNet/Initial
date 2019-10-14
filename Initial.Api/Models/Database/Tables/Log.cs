using System;
using System.ComponentModel.DataAnnotations;

namespace Initial.Api.Models.Database
{
    public class Log
    {
        [Key]
        public int Id { get; set; }

        public string Message { get; set; }

        public string MessageTemplate { get; set; }

        public DateTime TimeStamp { get; set; }

        [MaxLength(128)]
        public string Level { get; set; }

        public string Exception { get; set; }

        public string Properties { get; set; }
    }
}
