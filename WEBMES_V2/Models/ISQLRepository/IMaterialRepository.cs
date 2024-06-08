using WEBMES_V2.Models.DomainModels.MaterialStaging;
using WEBMES_V2.Models.DTO.MaterialStagingDTO;
using WEBMES_V2.Models.StaticModels.Generic;

namespace WEBMES_V2.Models.ISQLRepository
{
    public interface IMaterialRepository
    {
        Task<Boolean> CheckSidAvailable(StageLot stageLot);
        Task<Boolean> CheckSidInTracking(StageLot stageLot);
        Task<MtlMaterialThawingWorkLife> GetMaterialThawing(StageLot stageLot);
        Task<MtlMaterialTracking> Insert_MTL_Material_Tracking(MtlMaterialTracking mtlMaterialTracking);
        Task<MtlMaterialTracking> GetMaterialThawingTracking(StageLot stageLot);
        //Task<MtlMaterialTracking> UpdateThawDetails(StageLot stageLot);

        //Task<IEnumerable<MaterialHistoryDTO>> Get_Material_History(SearchData searchData);
        Task<IEnumerable<MaterialHistoryDTO>> Get_Material_Dashboard(StageLot stageLot);

    }
}
