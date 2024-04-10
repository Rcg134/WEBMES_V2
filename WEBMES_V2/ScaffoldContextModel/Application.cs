using System;
using System.Collections.Generic;

namespace WEBMES_V2.ScaffoldContextModel;

public partial class Application
{
    public long ApplicationCode { get; set; }

    public string? ApplicationId { get; set; }

    public string? ApplicationName { get; set; }

    public string? ApplicationLink { get; set; }

    public bool? Active { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? DateCreated { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? DateModified { get; set; }

    public string? Remarks { get; set; }
}
