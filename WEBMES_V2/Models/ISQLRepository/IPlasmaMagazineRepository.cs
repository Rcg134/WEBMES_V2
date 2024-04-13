using Microsoft.Data.SqlClient;
using WEBMES_V2.Models.DomainModels.PlasmaMagazine;
using WEBMES_V2.Models.DTO.PlasmaMagazineDTO;
using WEBMES_V2.Models.StaticModels.Generic;

namespace WEBMES_V2.Models.ISQLRepository
{
    public interface IPlasmaMagazineRepository
    {
        Task<IEnumerable<lotCheckingDTO>> CheckLotStage(StageLot stageLot);
        Task<Boolean> CheckMachine(StageLot stageLot);
        Task<Boolean> CheckLotinTRN_Lot_Magazine(StageLot stageLot);
        Task<TrnLotMagazine> Insert_TRN_Lot_Magazine(TrnLotMagazine trnLotMagazine);
        Task<TrnLotMagazineDTO> Get_InsertedId(StageLot stageLot);
        Task<IEnumerable<TrnMagazineDetailDTO>> GetMachineList(int id);
    }
}
