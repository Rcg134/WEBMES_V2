using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBMES_V2.Models.DomainModels.PlasmaMagazine;
using WEBMES_V2.Models.ISQLRepository;
using WEBMES_V2.Models.StaticModels.Generic;
using static WEBMES_V2.Models.StaticModels.Enums.ActionForStageEnum;
using static WEBMES_V2.Models.StaticModels.Enums.MagazineStageEnum;
using static WEBMES_V2.Models.StaticModels.Enums.StatusEnum;
using System.Security.Claims;
using WEBMES_V2.Models.DTO.PlasmaMagazineDTO;

namespace WEBMES_V2.Controllers
{
    [Authorize(Policy = "UserCred")]
    public class PlasmaMagazineController : Controller
    {
        private readonly IPlasmaMagazineRepository _plasmaMagazineRepository;

        public PlasmaMagazineController(IPlasmaMagazineRepository plasmaMagazineRepository)
        {
            this._plasmaMagazineRepository = plasmaMagazineRepository;
        }

        public IActionResult PlasmaMagazineView()
        {
            return View();
        }

        public async Task<IActionResult> WhatAction(int stageId,
                                                    StageLot stageLot)
        {
            return RedirectToAction("CheckLot",
                                     new {LotAlias = stageLot.LotAlias,
                                          stageCode= stageLot.StageCode
                                          });
        }
        public async Task<IActionResult> CheckLot(StageLot stagelot)
        {
            var StageCode = stagelot.StageCode;

            var lotDetails = await _plasmaMagazineRepository
                                            .CheckLotStage(stagelot);

    

            //Check if user select Plasma to WB or plasma to mold , check if lot was tracked out in DA cure and trackin in Plasma
            if (StageCode == (int)StageEnum.DA_CURE) {
                var isTrackoutInCure = lotDetails.Any(lot => lot.StageCode == (int)StageEnum.DA_CURE &&
                                                             lot.StatusCode == (int)StatusListEnum.LotComplete);

                var isTrackInInPlasma = lotDetails.Any(lot => lot.StageCode == (int)StageEnum.Plasma &&
                                                              lot.StatusCode == (int)StatusListEnum.InProcess);

                if (!isTrackoutInCure ||
                    !isTrackInInPlasma)
                {
                    return Json(new
                    {
                        status = 2,
                        details = (object)null,
                        message = $"Lot is yet process in DA Cure Or Not yet Trackin in Plasma"
                    });
                }

            }

            //validate if no record found
            if (lotDetails.Count() == 0 || 
                lotDetails == null)
                return Json(new
                {
                    status = 0,
                    details = (object)null,
                    message =  "Lot is not exist"
                });

          
            var isProcess = lotDetails
                     .FirstOrDefault(lot => lot.StageCode == StageCode && 
                                             lot.StatusCode == (int)StatusListEnum.InProcess || 
                                             lot.StatusCode == (int)StatusListEnum.LotComplete);

            //validate if  null and stage is not in process in DA Stage
            if (isProcess == null)
            {
                var lotdetails = lotDetails
                                    .SingleOrDefault(lot => lot.StageCode == StageCode);

                return Json(new
                {
                    status = 2,
                    details = (object)null,
                    message = $"Lot is currently in the {lotdetails.StatusID} stage"
                });
            }

            return Json(new {status = 1,
                             details = isProcess });
        }

        public async Task<IActionResult> CheckMachine(StageLot stagelot)
        {

            var isExist = await _plasmaMagazineRepository
                                             .CheckMachine(stagelot);

            var StatusCode = (int)StatusListEnum.InProcess;

            var insertedId = new TrnLotMagazine();
            var insertedIdDTO = new TrnLotMagazineDTO();


            //validate if not exist
            if (!isExist)
                return Json(new
                {
                    status = 0,
                    detail = (object)null,
                    message = "Machine is not exist Please Contact Pre Assy to add machine"
                });


            //Check if lot exist in TRN_Lot_Magazine
            var isLotExist = await _plasmaMagazineRepository
                                            .CheckLotinTRN_Lot_Magazine(stagelot);

            //if Lot does not exist then insert into TRN_Lot_Magazine then get Inserted ID
            if (!isLotExist)
            {
                var insertLotDetails = new TrnLotMagazine
                {
                    Lot = stagelot.LotAlias,
                    LotQty = stagelot.lotQTY,
                    MachineCode = stagelot.MachineCode,
                    TransactedBy = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    DateTimeStarted = DateTime.Now,
                    StatusRemarks = StatusCode.ToString()
                };

                insertedId = await _plasmaMagazineRepository.Insert_TRN_Lot_Magazine(insertLotDetails);

                return Json(new
                {
                    status = 1,
                    id = insertedId,
                    statusRemarks = insertedIdDTO.StatusRemarks,
                    message = "Machine is exist"
                });
            }

            //if lot already existed in TRN_Lot_Magazine get the Id
            insertedIdDTO = await _plasmaMagazineRepository.Get_InsertedId(stagelot);

            return Json(new
            {
                status = 1,
                id = insertedIdDTO.Id,
                statusRemarks = insertedIdDTO.StatusRemarks,
                message = "Machine is exist"
            });
        }

        public async Task<IActionResult> _MagazineTrackInList(int id)
        {
          var magazineList = await _plasmaMagazineRepository.GetMagazineList(id);
          return PartialView(magazineList);
        }

        public async Task<IActionResult> ValidateandInsertMagazine(StageLot stagelot)
        {
            var getMagazineDetail = await _plasmaMagazineRepository.Get_Magazine_MS_Station_Magazine(stagelot);

            //if Magazine is not exist in Trn_MagazineDetail table
            if (getMagazineDetail == null) 
            {
                return Json(new
                {
                    status = 0,
                    message = "Magazine is not existing please contact IT do add this magazine"
                });
            }


            var trnMagazineDTO = new TrnMagazineDetailDTO
            {
                TrnLotMagazineId = stagelot.id,
                MagazineCode = stagelot.MagazineCode,
                MagazineQty = getMagazineDetail.MagazineQty,
                StationId = stagelot.StageCode,
                StatusId = Convert.ToInt32(stagelot.StatusRemarks),
                CurrentScannedQty = 0,
                DateTimeScanned = DateTime.Now,
                ScannedBy = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                Remarks = stagelot.Remarks
            };

            var insertDTO = await _plasmaMagazineRepository
                                             .Insert_Magazine_Trn_MagazineDetail_and_History(trnMagazineDTO);

            return Json(insertDTO);
        }

    }
}
