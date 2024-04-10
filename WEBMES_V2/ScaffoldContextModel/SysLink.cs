using System;
using System.Collections.Generic;

namespace WEBMES_V2.ScaffoldContextModel;

public partial class SysLink
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Link1 { get; set; }

    public string? Link2 { get; set; }

    public string? Link3 { get; set; }

    public string? Link4 { get; set; }

    public string? RedirectLink1 { get; set; }

    public string? RedirectLink2 { get; set; }

    public string? RedirectLink3 { get; set; }

    public string? RedirectLink4 { get; set; }

    public int? DepartmentCode { get; set; }
}
