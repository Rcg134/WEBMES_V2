namespace WEBMES_V2.Models.DTO.PlasmaMagazineDTO
{
    public class TrnLotMagazineDTO
    {
        public int Id { get; set; }

        public int? Lot { get; set; }

        public int? LotQty { get; set; }

        public int? MachineCode { get; set; }

        public int? TransactedBy { get; set; }

        public DateTime? DateTimeStarted { get; set; }

        public string? StatusRemarks { get; set; }

    }
}
