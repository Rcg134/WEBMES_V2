using WEBMES_V2.Models.ISQLRepository;
using WEBMES_V2.Models.StaticModels.Generic;
using AutoMapper;
using WEBMES_V2.Models.Context;
using Microsoft.Data.SqlClient;
using System.Data;
using WEBMES_V2.Models.DTO.PlasmaMagazineDTO;
using WEBMES_V2.Models.StoreProcedures.PlasmaMagazineSP;
using Dapper;
using Microsoft.EntityFrameworkCore;
using WEBMES_V2.Models.DomainModels.MaterialStaging;
using WEBMES_V2.Models.DTO.MaterialStagingDTO;
using WEBMES_V2.Models.StoreProcedures.MaterialStagingSP;


namespace WEBMES_V2.Models.SQLRepositoryImplementation
{
    public class MaterialRepository : IMaterialRepository
    {
        #region Constructor
        private readonly IDapperConnection _dapperConnection;
        private readonly IMapper _mapper;
        private readonly MesAtecContext _mesAtecContext;

        public MaterialRepository(IDapperConnection dapperConnection, 
                                  IMapper mapper, 
                                  MesAtecContext mesAtecContext)
        {
            _dapperConnection = dapperConnection;
            _mapper = mapper;
            _mesAtecContext = mesAtecContext;
        }
        #endregion

        #region SID Checker
        public async Task<Boolean> CheckSidAvailable(StageLot stageLot)
        {
            var isSIDExist = await _mesAtecContext
                                     .MtlMaterialThawingWorkLives
                                     .AnyAsync(x => x.Sid == stageLot.SID &&
                                                    x.MaterialType == stageLot.MaterialType);
            
            return isSIDExist;

        }
        
        public async Task<Boolean> CheckSidInTracking(StageLot stageLot)
        {
            var isSIDThawIn = await _mesAtecContext
                                    .MtlMaterialTrackings
                                    .AnyAsync(x => x.Qrcode == stageLot.QRCode &&
                                                   x.Sid == stageLot.SID &&
                                                   x.Batch == stageLot.Batch &&
                                                   x.BatchNumber == stageLot.BatchNumber);

            return isSIDThawIn;
        }

        public async Task<MtlMaterialThawingWorkLife> GetMaterialThawing(StageLot stageLot)
        {
            var sidDetails = await _mesAtecContext
                                    .MtlMaterialThawingWorkLives
                                    .FirstOrDefaultAsync(x => x.Sid == stageLot.SID &&
                                                   x.MaterialType == stageLot.MaterialType);

            if (sidDetails != null)
            {
                return sidDetails;
            }

            return null;
        }

        public async Task<MtlMaterialTracking> Insert_MTL_Material_Tracking(MtlMaterialTracking insert_MTL_Material_TrackingDTO)
        {
            await _mesAtecContext.MtlMaterialTrackings.AddAsync(insert_MTL_Material_TrackingDTO);
            await _mesAtecContext.SaveChangesAsync();
            return insert_MTL_Material_TrackingDTO;
        }

        public async Task<MtlMaterialTracking> GetMaterialThawingTracking(StageLot stageLot)
        {
            var sidDetails = await _mesAtecContext
                                   .MtlMaterialTrackings
                                   .FirstOrDefaultAsync(x => x.Sid == stageLot.SID &&
                                                             x.Qrcode == stageLot.QRCode &&
                                                             x.Batch == stageLot.Batch &&
                                                             x.BatchNumber == stageLot.BatchNumber);
            if (sidDetails != null)
            {
                return sidDetails;
            }
            return null;
        }

        //public async Task<MtlMaterialTracking> UpdateThawDetails(StageLot stageLot)
        //{
        //    var sidDetails = await _mesAtecContext
        //                           .MtlMaterialTrackings
        //                           .FirstOrDefaultAsync(x => x.Sid == stageLot.SID &&
        //                                                     x.Qrcode == stageLot.QRCode &&
        //                                                     x.Batch == stageLot.Batch &&
        //                                                     x.BatchNumber == stageLot.BatchNumber);





        //    return null;
        //}






        #endregion

        #region Material History

        //public async Task<IEnumerable<MaterialHistoryDTO>> Get_Material_History(SearchData searchData)
        //{
        //    await using SqlConnection sqlConnection = _dapperConnection
        //                                                .CreateConnection();




        //}


        #endregion


        #region Material Dashboard
        public async Task<IEnumerable<MaterialHistoryDTO>> Get_Material_Dashboard(StageLot stageLot)
        {
            await using SqlConnection sqlConnection = _dapperConnection
                                                .CreateConnection();

            var materialHistoryDetails = await sqlConnection.QueryAsync<MaterialHistoryDTO>(
                                                                            MaterialStaging.usp_MTL_Material_History,
                                                                            new
                                                                            {
                                                                                MaterialType = stageLot.MaterialType
                                                                            },
                                                                            commandType: CommandType.StoredProcedure
                                                                            );

            if (materialHistoryDetails != null)
            {
                return materialHistoryDetails;
            }

            return null;
        }



        #endregion
    }
}
