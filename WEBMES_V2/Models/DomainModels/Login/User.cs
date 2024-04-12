using System;
using System.Collections.Generic;

namespace WEBMES_V2.Models.DomainModels.Login;

public partial class User
{
    public long UserCode { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public string? EmailAddress { get; set; }

    public bool? Active { get; set; }

    public DateTime? DateRegistered { get; set; }

    public DateTime? DateRegistrationVerified { get; set; }

    public long? RegistrationVerifiedBy { get; set; }

    public DateTime? DateModified { get; set; }

    public long? ModifiedBy { get; set; }

    public string? Remarks { get; set; }

    public string? EmpNo { get; set; }
}
