using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WEBMES_V2.Models.Context;
using WEBMES_V2.Models.DTO.PlasmaMagazineDTO;
using WEBMES_V2.Models.ISQLRepository;
using WEBMES_V2.Models.StaticModels.Generic;
using WEBMES_V2.Models.StoreProcedures.PlasmaMagazineSP;

namespace WEBMES_V2.Models.SQLRepositoryImplementation
{
    public class PlasmaMagazineRepository : IPlasmaMagazineRepository
    {
        #region Construnctor
        private readonly IDapperConnection _dapperConnection;
        private readonly MesAtecContext _mesAtecContext;

        public PlasmaMagazineRepository(IDapperConnection dapperConnection,
                                        MesAtecContext  mesAtecContext)
        {
            this._dapperConnection = dapperConnection;
            this._mesAtecContext = mesAtecContext;
        }


        #endregion

        #region Lot Checker
        public async Task<IEnumerable<lotCheckingDTO>> CheckLotStage(StageLot stageLot)
        {
            await using SqlConnection sqlConnection = _dapperConnection
                                                                .CreateConnection();

            var lotstageDetails = await sqlConnection.QueryAsync<lotCheckingDTO>(
                                                                          PlasmaMagazine.usp_Current_Station_Check,
                                                                          new 
                                                                          {
                                                                           lotAlias = stageLot.LotAlias
                                                                          },
                                                                          commandType: CommandType.StoredProcedure
                                                                          );

            if (lotstageDetails != null )
            {
                return lotstageDetails;
            }

            return null;
        }
        #endregion

        #region Machine Checker
        public async Task<bool> CheckMachine(StageLot stageLot)
        {
            return await _mesAtecContext.PsEquipments
                                     .AnyAsync(equipment => equipment.EquipmentId == stageLot.MachineCode);
        }
        #endregion

        public Task<lotCheckingDTO> CheckLotinTRN_Lot_Magazine(StageLot stageLot)
        {
            throw new NotImplementedException();
        }

        //public async Task<lotCheckingDTO> CheckLotinTRN_Lot_Magazine(StageLot stageLot)
        //{
        //    //return await _mesAtecContext.TrnLotMagazines.SingleOrDefaultAsync();
        //}
    }
}
