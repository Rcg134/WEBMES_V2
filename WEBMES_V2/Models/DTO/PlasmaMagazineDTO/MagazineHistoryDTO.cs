namespace WEBMES_V2.Models.DTO.PlasmaMagazineDTO
{
    public class MagazineHistoryDTO
    {
        public string Lot { get; set; }
        public string MagazineCode { get; set; }
        public int MagazineQty { get; set; }
        public string PackageId { get; set; }

        public string LeadCount { get; set; }
        public string StatusId { get; set; }
        public string StageId { get; set; }
        public DateTime DateTime_TrackIn { get; set; }
        public DateTime? DateTime_TrackOut { get; set; }
        public string ScannedBy { get; set; }
        public string Remarks { get; set; }
        public DateTime Due_Date { get; set; }
       
        public string Color { get; set; }

    }
}
