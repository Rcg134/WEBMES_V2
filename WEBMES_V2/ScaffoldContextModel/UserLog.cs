using System;
using System.Collections.Generic;

namespace WEBMES_V2.ScaffoldContextModel;

public partial class UserLog
{
    public long UserCode { get; set; }

    public string Activity { get; set; } = null!;

    public DateTime TransactionDate { get; set; }

    public string? Cpuno { get; set; }

    public string? MachineNo { get; set; }

    public string? Ipaddress { get; set; }
}
