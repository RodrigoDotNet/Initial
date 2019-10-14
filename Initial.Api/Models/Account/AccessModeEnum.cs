using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Initial.Api.Models.Account
{
    [Flags]
    public enum AccessModeEnum
    {
        Read = 1,
        Create = 2,
        Modify = 4,
        Delete = 8
    }
}
