using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WEBMES_V2.Models.Context;
using WEBMES_V2.Models.DomainModels.PlasmaMagazine;
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
        private readonly IMapper _mapper;

        public PlasmaMagazineRepository(IDapperConnection dapperConnection,
                                        MesAtecContext  mesAtecContext,
                                        IMapper mapper)
        {
            this._dapperConnection = dapperConnection;
            this._mesAtecContext = mesAtecContext;
            this._mapper = mapper;
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
        public async Task<Boolean> CheckMachine(StageLot stageLot)
        {
            return await _mesAtecContext.PsEquipments
                                     .AnyAsync(equipment => equipment.EquipmentId == Convert.ToString(stageLot.MachineCode));
        }
        #endregion

        public async Task<Boolean> CheckLotinTRN_Lot_Magazine(StageLot stageLot)
        {
   
                var trnLotMagazineDetails = await _mesAtecContext.TrnLotMagazines
                                                     .AsNoTracking()
                                                     .SingleOrDefaultAsync(lot => lot.Lot == stageLot.LotAlias &&
                                                                                  lot.MachineCode == stageLot.MachineCode);
                if (trnLotMagazineDetails != null)
                    return true;

            return false;
        }

        public async Task<Boolean> Insert_TRN_Lot_Magazine(TrnLotMagazine insert_TRN_Lot_MagazineDT0)
        {
            await _mesAtecContext.TrnLotMagazines.AddAsync(insert_TRN_Lot_MagazineDT0);
            await _mesAtecContext.SaveChangesAsync();
            return true;
        }

   
    }
}
