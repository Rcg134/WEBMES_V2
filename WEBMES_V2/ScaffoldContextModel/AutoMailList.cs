using System;
using System.Collections.Generic;

namespace WEBMES_V2.ScaffoldContextModel;

public partial class AutoMailList
{
    public string ProcessName { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public bool? MailTo { get; set; }

    public bool? MailCc { get; set; }

    public bool? MailFrom { get; set; }
}
