using System;
using System.Collections.Generic;

namespace WEBMES_V2.Models.DomainModels.PlasmaMagazine;

public partial class TrnLotMagazine
{
    public int Id { get; set; }

    public string? Lot { get; set; }

    public int? LotQty { get; set; }

    public string? MachineCode { get; set; }

    public int? TransactedBy { get; set; }

    public DateTime? DateTimeTrackIn { get; set; }

    public DateTime? DateTimeTrackOut { get; set; }

    public string? StatusRemarks { get; set; }

    public int? CurrentTrackOutQty { get; set; }
}
