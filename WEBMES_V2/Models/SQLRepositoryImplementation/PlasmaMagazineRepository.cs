
using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Data;
using System.Xml.Linq;
using WEBMES_V2.Models.Context;
using WEBMES_V2.Models.DomainModels.PlasmaMagazine;
using WEBMES_V2.Models.DTO.PlasmaMagazineDTO;
using WEBMES_V2.Models.ISQLRepository;
using WEBMES_V2.Models.StaticModels.Generic;
using WEBMES_V2.Models.StoreProcedures.PlasmaMagazineSP;
using static WEBMES_V2.Models.StaticModels.Enums.MagazineStageEnum;

namespace WEBMES_V2.Models.SQLRepositoryImplementation
{
    public class PlasmaMagazineRepository : IPlasmaMagazineRepository
    {
        #region Construnctor
        private readonly IDapperConnection _dapperConnection;
        private readonly MesAtecContext _mesAtecContext;
        private readonly IMapper _mapper;

        public PlasmaMagazineRepository(IDapperConnection dapperConnection,
                                        MesAtecContext mesAtecContext,
                                        IMapper mapper)
        {
            this._dapperConnection = dapperConnection;
            this._mesAtecContext = mesAtecContext;
            this._mapper = mapper;
        }


        #endregion

        #region Lot Checker
        public async Task<Boolean> CheckLotStageiFHold(StageLot stageLot)
        {
            await using SqlConnection sqlConnection = _dapperConnection
                                                                .CreateConnection();

            var statusCode = await sqlConnection.QueryFirstOrDefaultAsync<StageLot>(
                                                                  PlasmaMagazine.usp_Check_Lot_If_Hold,
                                                                  new
                                                                  {
                                                                      Lot = stageLot.LotAlias,
                                                                  },
                                                                  commandType: CommandType.StoredProcedure
                                                                  );

            if (statusCode.StatusCode == 4)
            {
                return true;
            }

            return false;
        }
        public async Task<IEnumerable<lotCheckingDTO>> CheckLotStage(StageLot stageLot)
        {
            await using SqlConnection sqlConnection = _dapperConnection
                                                                .CreateConnection();

            var lotstageDetails = await sqlConnection.QueryAsync<lotCheckingDTO>(
                                                                          PlasmaMagazine.usp_Current_Station_Check,
                                                                          new
                                                                          {
                                                                              lotAlias = stageLot.LotAlias,
                                                                              @StationID = stageLot.StageCode
                                                                          },
                                                                          commandType: CommandType.StoredProcedure
                                                                          );

            if (lotstageDetails != null)
            {
                return lotstageDetails;
            }

            return null;
        }


        public async Task<TrnLotMagazineDTO> Check_Lot_if_Exist(StageLot stageLot)
        {
            var lotDetails = await _mesAtecContext
                                         .TrnLotMagazines
                                         .FirstOrDefaultAsync(lot => lot.Lot == stageLot.LotAlias);

            if (lotDetails != null )
                return _mapper.Map<TrnLotMagazineDTO>(lotDetails);

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
        public async Task<TrnLotMagazine> Insert_TRN_Lot_Magazine(TrnLotMagazine insert_TRN_Lot_MagazineDT0)
        {
            insert_TRN_Lot_MagazineDT0.StatusRemarks = "0";
            insert_TRN_Lot_MagazineDT0.CurrentTrackOutQty = 0;
            await _mesAtecContext.TrnLotMagazines.AddAsync(insert_TRN_Lot_MagazineDT0);
            await _mesAtecContext.SaveChangesAsync();
            return insert_TRN_Lot_MagazineDT0;
        }
        public async Task<TrnLotMagazineDTO> Get_InsertedId(StageLot stageLot)
        {
            var GetId = await _mesAtecContext
                                        .TrnLotMagazines
                                        .FirstOrDefaultAsync(id => id.Lot == stageLot.LotAlias);

            if (GetId != null)
                return _mapper.Map<TrnLotMagazineDTO>(GetId);

            return null;
        }
        public async Task<int> Get_CurrentTrackoutQTY(StageLot stageLot)
        {
            var GetQTY =  _mesAtecContext
                                        .TrnMagazineDetailsHistories
                                        .Where(history => history.StationId == stageLot.StageCode &&
                                                          history.StatusId == 5 &&
                                                          history.DateTimeTrackOut != null &&
                                                          history.TrnLotMagazineId == stageLot.TRN_Lot_Magzine_Id)
                                        .AsNoTracking().Count();

            
            return GetQTY;
        }
        public async Task<IEnumerable<TrnMagazineDetailViewDTO>> GetMagazineList(StageLot stageLot)
        {
            await using SqlConnection sqlConnection = _dapperConnection
                                                                         .CreateConnection();

            var magazineDetails = await sqlConnection.QueryAsync<TrnMagazineDetailViewDTO>(
                                                                          PlasmaMagazine.usp_Get_Magazine_List,
                                                                          new
                                                                          {
                                                                              transactionId = stageLot.id,
                                                                              StageCode = stageLot.StageCode
                                                                          },
                                                                          commandType: CommandType.StoredProcedure
                                                                          );

            if (magazineDetails != null)
            {
                return magazineDetails;
            }

            return null;

        }
        public async Task<MsStationMagazineDTO> Get_Magazine_MS_Station_Magazine(StageLot stageLot)
        {
            try { 

            var magazineDetail = await _mesAtecContext
                                           .MsStationMagazines
                                           .FirstOrDefaultAsync(mag => mag.Id == stageLot.PackageTransId);

            if (magazineDetail != null)
            {
                return _mapper.Map<MsStationMagazineDTO>(magazineDetail);
            }

            return null;
            }

            catch (Exception ex) { }
            return null;
        }
        public async Task<TrnMagazineDetailDTO> Get_Magazine_Trn_MagazineDetail(StageLot stageLot)
        {
            var magazineDetail = await _mesAtecContext
                                       .TrnMagazineDetails
                                       .FirstOrDefaultAsync(mag => mag.TrnLotMagazineId == stageLot.TRN_Lot_Magzine_Id &&
                                                                   mag.MagazineCode == stageLot.MagazineCode);

            if (magazineDetail != null)
            {
                return _mapper.Map<TrnMagazineDetailDTO>(magazineDetail);
            }

            return null;
        }
        public async Task<InsertValidate> Insert_Magazine_Trn_MagazineDetail_and_History(TrnMagazineDetailDTO trnMagazineDetailDTO)
        {

            var isExistinTrnMagazineDetails = await _mesAtecContext
                                                           .TrnMagazineDetails
                                                           .FirstOrDefaultAsync(isExist => isExist.TrnLotMagazineId == trnMagazineDetailDTO.TrnLotMagazineId &&
                                                                                isExist.MagazineCode == trnMagazineDetailDTO.MagazineCode);


            if (isExistinTrnMagazineDetails != null) 
            { 
                    var existedCurrentMagazineStation = Enum.GetName(typeof(StageEnum), (int)isExistinTrnMagazineDetails.StationId);
                    return new InsertValidate()
                    {
                        isInserted = false,
                        message = $"Magazine {isExistinTrnMagazineDetails.MagazineCode} is already TRACKED IN in {existedCurrentMagazineStation} stage (already used)"
                    };
            }

                var insertDetails = _mapper.Map<TrnMagazineDetail>(trnMagazineDetailDTO);
                var insertDetailsHistory = _mapper.Map<TrnMagazineDetailsHistory>(trnMagazineDetailDTO);
                _mesAtecContext.TrnMagazineDetails.Add(insertDetails);
                _mesAtecContext.TrnMagazineDetailsHistories.Add(insertDetailsHistory);
                await _mesAtecContext.SaveChangesAsync();
                return new InsertValidate()
                {
                    isInserted = true,
                    message = ""
                };

        }
        public async Task<InsertValidate> TrackOut(StageLot stageLot)
        {
            await using SqlConnection sqlConnection = _dapperConnection
                                                                  .CreateConnection();

            var isSuccess = await sqlConnection.QueryFirstOrDefaultAsync<InsertValidate>(
                                                                            PlasmaMagazine.usp_Update_Magazine_Status,
                                                                            new
                                                                            {
                                                                                StatusId = stageLot.StatusCode,
                                                                                TransactionId = stageLot.TRN_Lot_Magzine_Id,
                                                                                StationId = stageLot.StageCode,
                                                                                MagazineCode = stageLot.MagazineCode,
                                                                            },
                                                                            commandType: CommandType.StoredProcedure
                                                                            );
                                                                            
            if (isSuccess != null)
            {
                return isSuccess;
            }

            return null;
        }
        public async Task<bool> Insert_XML_MS_Station_Magazine(XDocument MagazineXML)
        {
            await using SqlConnection sqlConnection = _dapperConnection
                                                    .CreateConnection();

            var lotstageDetails = await sqlConnection.QueryAsync<lotCheckingDTO>(
                                                                          PlasmaMagazine.usp_MS_Station_Magazine_XML_Insert,
                                                                          new
                                                                          {
                                                                              XMLdoc = MagazineXML
                                                                          },
                                                                          commandType: CommandType.StoredProcedure
                                                                          );

            if (lotstageDetails != null)
            {
                return true;
            }

            return false;

        }
        public async Task<IEnumerable<MsStationMagazineDTO>> Get_Package_List(StageLot stageLot)
        {
            var packageDetails =  _mesAtecContext
                                         .MsStationMagazines
                                         .Where(package => package.CustomerId == stageLot.CustomerID)
                                         .AsNoTracking();
            if (packageDetails != null)
            {
                var packageList = await packageDetails.ToListAsync();
                return _mapper.Map<IEnumerable<MsStationMagazineDTO>>(packageList);
            }

            return null;

        }
        public async Task<IEnumerable<MagazineHistoryDTO>> Get_Magazine_History(SearchData searchData)
        {
            await using SqlConnection sqlConnection = _dapperConnection
                                                        .CreateConnection();

            var magazineHistoryDetails = await sqlConnection.QueryAsync<MagazineHistoryDTO>(
                                                                            PlasmaMagazine.usp_Magazine_History_Search,
                                                                            new
                                                                            {
                                                                                SearchData = searchData.searchValue,
                                                                                StageValue = searchData.stagePlanSelect
                                                                            },
                                                                            commandType: CommandType.StoredProcedure
                                                                            );

            if (magazineHistoryDetails != null)
            {
                return magazineHistoryDetails;
            }

            return null;
        }

        public async Task<IEnumerable<MagazineHistoryDTO>> Get_Magazine_DashBoard(StageLot stageLot)
        {
            await using SqlConnection sqlConnection = _dapperConnection
                                               .CreateConnection();

            var magazineHistoryDetails = await sqlConnection.QueryAsync<MagazineHistoryDTO>(
                                                                            PlasmaMagazine.usp_Magazine_DashBoard,
                                                                            new
                                                                            {
                                                                                StageID = stageLot.StageCode
                                                                            },
                                                                            commandType: CommandType.StoredProcedure
                                                                            );

            if (magazineHistoryDetails != null)
            {
                return magazineHistoryDetails;
            }

            return null;
        }
    }
}
