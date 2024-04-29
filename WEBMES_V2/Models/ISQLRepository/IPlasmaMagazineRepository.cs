using Microsoft.Data.SqlClient;
using System.Xml.Linq;
using WEBMES_V2.Models.DomainModels.PlasmaMagazine;
using WEBMES_V2.Models.DTO.PlasmaMagazineDTO;
using WEBMES_V2.Models.StaticModels.Generic;

namespace WEBMES_V2.Models.ISQLRepository
{
    public interface IPlasmaMagazineRepository
    {
        Task<Boolean> CheckLotStageiFHold(StageLot stageLot);
        Task<IEnumerable<lotCheckingDTO>> CheckLotStage(StageLot stageLot);
        Task<Boolean> CheckMachine(StageLot stageLot);
        Task<Boolean> CheckLotinTRN_Lot_Magazine(StageLot stageLot);
        Task<TrnLotMagazine> Insert_TRN_Lot_Magazine(TrnLotMagazine trnLotMagazine);
        Task<TrnLotMagazineDTO> Get_InsertedId(StageLot stageLot);
        Task<int> Get_CurrentTrackoutQTY(StageLot stageLot);
        Task<IEnumerable<TrnMagazineDetailViewDTO>> GetMagazineList(StageLot stageLot);
        Task<MsStationMagazineDTO> Get_Magazine_MS_Station_Magazine(StageLot stageLot);
        Task<TrnMagazineDetailDTO> Get_Magazine_Trn_MagazineDetail(StageLot stageLot);
        Task<IEnumerable<MsStationMagazineDTO>> Get_Package_List(StageLot stageLot);
        Task<InsertValidate> Insert_Magazine_Trn_MagazineDetail_and_History(TrnMagazineDetailDTO trnMagazineDetailDTO);
        Task<InsertValidate> TrackOut(StageLot stageLot);
        Task<Boolean> Insert_XML_MS_Station_Magazine(XDocument MagazineXML);
        Task<IEnumerable<MagazineHistoryDTO>> Get_Magazine_History(SearchData searchData);

    }
}
