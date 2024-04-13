﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace WEBMES_V2.Models.DTO.PlasmaMagazineDTO
{
    public class lotCheckingDTO
    {
        public string? LotAlias { get; set; }
        public int? LotCode { get; set; }
        public int? StageCode { get; set; }
        public string? StageID { get; set; }
        public string? Sequence { get; set; }
        public int? StatusCode { get; set; }
        public string? StatusID { get; set; }
        public int? QTY { get; set; }

    }
}