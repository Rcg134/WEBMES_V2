namespace WEBMES_V2.Models.DTO.MaterialStagingDTO
{
    public class MaterialHistoryDTO
    {     
        public string MaterialDesc { get; set; }
        public string BatchNumber { get; set; }
        public string Customer { get; set; }
        public DateTime ThawIn { get; set; }
        public DateTime ThawOutEnd { get; set; }
        public DateTime WorkLifeEnd { get; set; }
        public string ExpirationDate { get; set; }
        public string TimeLeft { get; set; }
        public string Remarks { get; set; }
        public string Color { get; set; }

    }
}
