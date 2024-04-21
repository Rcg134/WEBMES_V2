using System;
using System.Collections.Generic;

namespace WEBMES_V2.Models.DomainModels.PlasmaMagazine;

public partial class TrnMagazineDetailsHistory
{
    public int Id { get; set; }

    public int? TrnLotMagazineId { get; set; }

    public string? MagazineCode { get; set; }

    public int? MagazineQty { get; set; }

    public int? PackageId { get; set; }

    public int? StatusId { get; set; }

    public int? StationId { get; set; }

    public int? DateTimeTrackIn { get; set; }

    public int? DateTimeTrackOut { get; set; }

    public DateTime? DateTimeScanned { get; set; }

    public int? ScannedBy { get; set; }

    public string? Remarks { get; set; }
}
