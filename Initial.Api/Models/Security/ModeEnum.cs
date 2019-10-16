using System;

namespace Initial.Api.Models
{
    [Flags]
    public enum ModeEnum
    {
        None = 0,

        Read = 1,

        Create = 2,

        Modify = 4,

        Delete = 8,

        All = 1 + 2 + 4 + 8
    }
}
