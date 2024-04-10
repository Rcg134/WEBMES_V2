using System;
using System.Collections.Generic;

namespace WEBMES_V2.ScaffoldContextModel;

public partial class VwUserAccess
{
    public long UserCode { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? EmailAddress { get; set; }

    public string? FullName { get; set; }

    public short? UserGroupCode { get; set; }

    public string? UserGroupId { get; set; }

    public long ApplicationCode { get; set; }

    public string? ApplicationId { get; set; }

    public string? ApplicationName { get; set; }

    public DateTime? DateRegistrationVerified { get; set; }

    public long? RegistrationVerifiedBy { get; set; }

    public bool? Active { get; set; }
}
