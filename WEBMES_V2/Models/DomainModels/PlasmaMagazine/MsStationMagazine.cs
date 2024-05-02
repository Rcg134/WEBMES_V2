using System;
using System.Collections.Generic;

namespace WEBMES_V2.Models.DomainModels.PlasmaMagazine;

public partial class MsStationMagazine
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public string? MagazineCode { get; set; }

    public string? PackageId { get; set; }

    public string? LeadCount { get; set; }

    public int? MagazineQty { get; set; }

    public double? LifeSpanDay { get; set; }

    public int? StationId { get; set; }

    public double? Tyellow { get; set; }

    public double? Torange { get; set; }

    public double? Tred { get; set; }

    public DateTime? TimeCreated { get; set; }
}
