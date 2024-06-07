using System;
using System.Collections.Generic;

namespace WEBMES_V2.Models.DomainModels.MaterialStaging;

public partial class MtlMaterialThawingWorkLife
{
    public int Id { get; set; }

    public int? MaterialType { get; set; }

    public string? Sid { get; set; }

    public string? ProductType { get; set; }

    public int? ThawingTime { get; set; }

    public int? WorkLife { get; set; }
}
