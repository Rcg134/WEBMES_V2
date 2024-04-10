using System;
using System.Collections.Generic;

namespace WEBMES_V2.ScaffoldContextModel;

public partial class ApplicationDetailsMvc
{
    public int Id { get; set; }

    public int? ApplicationCode { get; set; }

    public string? Icon { get; set; }

    public int? Sequence { get; set; }

    public bool? IsDirectory { get; set; }
}
