using System;
using System.Collections.Generic;

namespace WEBMES_V2.ScaffoldContextModel;

public partial class UserAccess033120
{
    public long UserCode { get; set; }

    public long ApplicationCode { get; set; }

    public long GroupCode { get; set; }

    public DateTime? DateRegistrationVerified { get; set; }

    public long? RegistrationVerifiedBy { get; set; }
}
