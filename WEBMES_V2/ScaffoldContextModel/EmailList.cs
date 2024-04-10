using System;
using System.Collections.Generic;

namespace WEBMES_V2.ScaffoldContextModel;

public partial class EmailList
{
    public int EmailCode { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string? FullName { get; set; }

    public DateTime? DateModified { get; set; }

    public bool? Active { get; set; }

    public string? Dept { get; set; }
}
