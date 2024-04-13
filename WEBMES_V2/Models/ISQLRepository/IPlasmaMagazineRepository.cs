using Microsoft.Data.SqlClient;
using WEBMES_V2.Models.DTO.PlasmaMagazineDTO;
using WEBMES_V2.Models.StaticModels.Generic;

namespace WEBMES_V2.Models.ISQLRepository
{
    public interface IPlasmaMagazineRepository
    {
        Task<IEnumerable<lotCheckingDTO>> CheckLotStage(StageLot stageLot);
        Task<Boolean> CheckMachine(StageLot stageLot);
        Task<lotCheckingDTO> CheckLotinTRN_Lot_Magazine(StageLot stageLot);
    }
}
