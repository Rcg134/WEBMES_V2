namespace WEBMES_V2.Models.DTO.PlasmaMagazineDTO
{
    public class MsStationMagazineDTO
    {
        public int Id { get; set; }

        public string? MagazineCode { get; set; }

        public int? PackageId { get; set; }

        public string? LeadCount { get; set; }

        public int? MagazineQty { get; set; }

        public int? LifeSpanDay { get; set; }

        public DateTime? TimeCreated { get; set; }
    }
}
