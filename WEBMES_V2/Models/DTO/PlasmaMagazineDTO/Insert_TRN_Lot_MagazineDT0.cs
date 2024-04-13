namespace WEBMES_V2.Models.DTO.PlasmaMagazineDTO
{
    public class Insert_TRN_Lot_MagazineDT0
    {

        public int Id { get; set; }

        public string? Lot { get; set; }

        public int? LotQty { get; set; }

        public int? MachineCode { get; set; }

        public int? TransactedBy { get; set; }

        public DateTime? DateTimeStarted { get; set; }

        public string? StatusRemarks { get; set; }

    }
}
