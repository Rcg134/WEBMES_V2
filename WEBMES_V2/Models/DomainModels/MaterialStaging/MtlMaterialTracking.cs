using System;
using System.Collections.Generic;

namespace WEBMES_V2.Models.DomainModels.MaterialStaging;

public partial class MtlMaterialTracking
{
    public int Id { get; set; }
    public string? Qrcode { get; set; }
    public string? Sid { get; set; }
    public string? MaterialDesc { get; set; }
    public string? Batch { get; set; }
    public string? BatchNumber { get; set; }
    public DateTime? ThawIn { get; set; }
    public int? ThawInBy { get; set; }
    public DateTime? ThawOut { get; set; }
    public int? ThawOutBy { get; set; }
    public DateTime? ThawOutEnd { get; set; }
    public DateTime? WorkLifeEnd { get; set; }
    public string? ExpirationDate { get; set; }
    public int? MaterialType { get; set; }
}
