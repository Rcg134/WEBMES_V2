using System;
using System.Collections.Generic;

namespace WEBMES_V2.ScaffoldContextModel;

public partial class Group
{
    public short UserGroupCode { get; set; }

    public string? UserGroupId { get; set; }

    public string? UserGroupDesc { get; set; }

    public bool? Active { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? DateCreated { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? DateModified { get; set; }

    public string? Remarks { get; set; }
}
