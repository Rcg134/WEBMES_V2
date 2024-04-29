namespace WEBMES_V2.Models.StaticModels.Generic
{
    public class StageLot
    {
        public int? id { get; set; }
        public string? LotAlias { get; set; }
        public string? MachineCode { get; set; }
        public int? lotQTY { get; set; }

        public int? PackageTransId { get; set; }
        public int? StatusCode { get; set; }
        public int? StageCode { get; set; }
        public int? TRN_Lot_Magzine_Id { get; set; }
        public int? CustomerID { get; set; }
        public int? CurrentTrackOutQTY { get; set; }

        public string? MagazineCode { get; set; }
        public string? StatusRemarks { get; set; }
        public string? Remarks { get; set; }
    }
}
