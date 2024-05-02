using System;
using System.Collections.Generic;

namespace WEBMES_V2.Models.DomainModels.PlasmaMagazine;

public partial class MsStationListMagazine
{
    public int Id { get; set; }

    public int? StationId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool? Active { get; set; }
}
