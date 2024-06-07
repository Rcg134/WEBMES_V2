namespace WEBMES_V2.Models.DTO.MaterialStagingDTO
{
    public class MtlMaterialThawingWorkLifeDTO
    {
        public int ID { get; set; }
        public int? MaterialType { get; set; }
        public string? SID { get; set; }
        public string? ProductType { get; set; }
        public int? ThawingTime { get; set; }
        public int? WorkLife { get; set; }
    }
}
