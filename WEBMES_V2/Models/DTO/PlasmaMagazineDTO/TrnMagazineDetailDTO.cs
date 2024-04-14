﻿namespace WEBMES_V2.Models.DTO.PlasmaMagazineDTO
{
    public class TrnMagazineDetailDTO
    {
        public int Id { get; set; }

        public int? TrnLotMagazineId { get; set; }

        public string? MagazineCode { get; set; }

        public int? MagazineQty { get; set; }

        public int? StatusId { get; set; }

        public int? StationId { get; set; }

        public int? CurrentScannedQty { get; set; }

        public DateTime? DateTimeScanned { get; set; }

        public int? ScannedBy { get; set; }

        public string? Remarks { get; set; }
    }
}
