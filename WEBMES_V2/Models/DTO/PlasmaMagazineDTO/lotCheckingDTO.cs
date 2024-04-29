using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace WEBMES_V2.Models.DTO.PlasmaMagazineDTO
{
    public class lotCheckingDTO
    {
        public string? LotAlias { get; set; }
        public int? LotCode { get; set; }
        public int? CustomerCode  { get; set; }
        public string? CustomerName { get; set; }
        public int? StageCode { get; set; }
        public string? StageID { get; set; }
        public string? Sequence { get; set; }
        public int? StatusCode { get; set; }
        public string? StatusID { get; set; }
        public int? QTY { get; set; }
        public string? PackageType { get; set; }
        public string? LeadType { get; set; }
        public string? PackageID { get; set; }
        public string? MagazineQTY { get; set; }
        

    }
}
