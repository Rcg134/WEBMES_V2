using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBMES_V2.Models.DomainModels.PlasmaMagazine;
using WEBMES_V2.Models.ISQLRepository;
using WEBMES_V2.Models.StaticModels.Generic;
using static WEBMES_V2.Models.StaticModels.Enums.ActionForStageEnum;
using static WEBMES_V2.Models.StaticModels.Enums.MagazineStageEnum;
using static WEBMES_V2.Models.StaticModels.Enums.StatusEnum;
using System.Security.Claims;

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
            var selectedAction = (ActionStageEnum)stageId;
            return RedirectToAction(selectedAction.ToString(),
                                     new {LotAlias = stageLot.LotAlias });
        }

        public async Task<IActionResult> CheckLot(StageLot stagelot)
        {
            var StageCode = (int)StageEnum.DA;

            var StatusCode = (int)StatusListEnum.InProcess;

            var lotDetails = await _plasmaMagazineRepository
                                            .CheckLotStage(stagelot);

            //validate if null
            if (lotDetails.Count() == 0 || 
                lotDetails == null)
                return Json(new
                {
                    status = 0,
                    details = (object)null,
                    message =  "Lot is not exist"
                });

           
            var isProcess = lotDetails
                     .SingleOrDefault(lot => lot.StageCode == StageCode && 
                                             lot.StatusCode == StatusCode);

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
                }); ;
            }

            return Json(new {status = 1,
                             details = isProcess });
        }

        public async Task<IActionResult> CheckMachine(StageLot stagelot)
        {

            var isExist = await _plasmaMagazineRepository
                                             .CheckMachine(stagelot);

            var StatusCode = (StatusListEnum)2;


            //validate if not exist
            if (!isExist)
                return Json(new
                {
                    status = 0,
                    detail = (object)null,
                    message = "Machine is not exist Please Contact Pre Assy to add machine"
                });


            //Insert in TRN_Lot_Magazine
            var isLotExist = await _plasmaMagazineRepository
                                            .CheckLotinTRN_Lot_Magazine(stagelot);

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

                await _plasmaMagazineRepository.Insert_TRN_Lot_Magazine(insertLotDetails);
             }


            return Json(new
            {
                status = 1,
                message = "Machine is exist"
            });
        }
    }
}
