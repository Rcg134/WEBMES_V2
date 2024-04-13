using System;
using System.Collections.Generic;

namespace WEBMES_V2.Models.DomainModels.PlasmaMagazine;

public partial class MsStationMagazine
{
    public int Id { get; set; }

    public int? MagazineCode { get; set; }

    public int? MagazineQty { get; set; }

    public int? LifeSpanDay { get; set; }

    public DateTime? TimeCreated { get; set; }
}
