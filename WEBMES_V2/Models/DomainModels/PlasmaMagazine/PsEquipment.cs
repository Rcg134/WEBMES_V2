using System;
using System.Collections.Generic;

namespace WEBMES_V2.Models.DomainModels.PlasmaMagazine;

public partial class PsEquipment
{
    public long EquipmentCode { get; set; }

    public string EquipmentId { get; set; } = null!;

    public string EquipmentDescription { get; set; } = null!;

    public int EquipmentTypeCode { get; set; }

    public int AreaCode { get; set; }

    public bool? Active { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
