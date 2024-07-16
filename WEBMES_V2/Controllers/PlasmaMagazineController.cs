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
using WEBMES_V2.Models.StaticModels.Dictionary;

namespace WEBMES_V2.Controllers
{
    [Authorize(Policy = "UserCred")]
    public class PlasmaMagazineController : Controller
    {
        private readonly IPlasmaMagazineRepository _plasmaMagazineRepository;
        private readonly IXMLConverter _xMLConverter;
        private readonly IDownloadFile _downloadFile;

        public PlasmaMagazineController(IPlasmaMagazineRepository plasmaMagazineRepository,
                                        IXMLConverter xMLConverter,
                                        IDownloadFile downloadFile)
        {
            this._plasmaMagazineRepository = plasmaMagazineRepository;
            this._xMLConverter = xMLConverter;
            this._downloadFile = downloadFile;
        }

        #region Plasma
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

            var ishold =await _plasmaMagazineRepository.CheckLotStageiFHold(stagelot);

            if (ishold)
            {
                return Json(new
                {
                    status = 3,
                    details = (object)null,
                    message = $"Lot is currently on hold"
                });
            }


            var lotDetails = await _plasmaMagazineRepository
                                            .CheckLotStage(stagelot);

    

            //Check if user select Plasma to WB or plasma to mold , check if lot was tracked out in DA cure and trackin in Plasma
            if (StageCode == (int)StageEnum.DA_CURE) {
                var isTrackoutInCure = lotDetails.Any(lot => lot.StageCode == (int)StageEnum.DA &&
                                                             lot.StatusCode == (int)StatusListEnum.InProcess);

                if (!isTrackoutInCure)
                {
                    return Json(new
                    {
                        status = 2,
                        details = (object)null,
                        message = $"Lot is not yet Tracked In in Die Attach"
                    });
                }

            }
            else
            {
                var isTrackInInPlasma = lotDetails.Any(lot => lot.StageCode == (int)StageEnum.Plasma &&
                                                       lot.StatusCode == (int)StatusListEnum.InProcess);


                if (!isTrackInInPlasma)
                {
                    return Json(new
                    {
                        status = 2,
                        details = (object)null,
                        message = $"Lot is not yet Tracked In Plasma"
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
                    message =  "Lot does not exist"
                });

                //get current stage
               var currentStageCode = StageDict.stageDict.ContainsKey((int)StageCode);
          
               var isProcess = lotDetails
                     .FirstOrDefault(lot => currentStageCode && 
                                            lot.StatusCode == (int)StatusListEnum.InProcess);


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
            //Return Details if exist
            var isExistDetails =await _plasmaMagazineRepository.Check_Lot_if_Exist(stagelot);

            if (isExistDetails == null)
            {
                return Json(new
                {
                    status = 1,
                    details = isProcess,
                    machineCode = ""
                });
            }

            return Json(new {status = 1,
                             details = isProcess,
                             machineCode = isExistDetails.MachineCode});
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
                    DateTimeTrackIn = DateTime.Now,
                    StatusRemarks = StatusCode.ToString()
                };

                insertedId = await _plasmaMagazineRepository.Insert_TRN_Lot_Magazine(insertLotDetails);

                //Check if lot is new TrackOut QTY
                stagelot.TRN_Lot_Magzine_Id = insertedId.Id;
                var trackOutnotExisIdtQTY = await _plasmaMagazineRepository.Get_CurrentTrackoutQTY(stagelot);

                return Json(new
                {
                    status = 1,
                    id = insertedId.Id,
                    statusRemarks = insertedIdDTO.StatusRemarks,
                    TrackoutQTY = trackOutnotExisIdtQTY,
                    message = "Machine is exist"
                });
            }

            //if lot already existed in TRN_Lot_Magazine get the Id
            insertedIdDTO = await _plasmaMagazineRepository.Get_InsertedId(stagelot);

            //Check if lot is already existed TrackOut QTY
            stagelot.TRN_Lot_Magzine_Id = insertedIdDTO.Id;
            var trackOutExisIdtQTY = await _plasmaMagazineRepository.Get_CurrentTrackoutQTY(stagelot);

            return Json(new
            {
                status = 1,
                id = insertedIdDTO.Id,
                statusRemarks = insertedIdDTO.StatusRemarks,
                TrackoutQTY = trackOutExisIdtQTY,
                message = "Machine is exist"
            });
        }

        public async Task<IActionResult> _MagazineTrackInList(StageLot stageLot)
        {
          var magazineList = await _plasmaMagazineRepository.GetMagazineList(stageLot);
          return PartialView(magazineList);
        }

        public async Task<IActionResult> _PackageModal(StageLot stageLot)
        {
            var packageList = await _plasmaMagazineRepository.Get_Package_List(stageLot);
            ViewBag.packageList = packageList;
            return PartialView();
        }

        public async Task<IActionResult> _TrackoutModal(StageLot stageLot)
        {
            ViewBag.transactioId = stageLot.id;
            ViewBag.packageList = stageLot.id;
            return PartialView();
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
                    message = "Package is not registered please upload it"
                });
            }


            var trnMagazineDTO = new TrnMagazineDetailDTO
            {
                TrnLotMagazineId = stagelot.id,
                MagazineCode = stagelot.MagazineCode,
                MagazineQty = getMagazineDetail.MagazineQty,
                StationId = stagelot.StageCode,
                PackageId = stagelot.PackageTransId,
                StatusId = (int)StatusListEnum.InProcess,
                CurrentScannedQty = 0,
                DateTimeTrackIn = DateTime.Now,
                ScannedBy = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                Remarks = stagelot.Remarks
            };

            var insertDTO = await _plasmaMagazineRepository
                                             .Insert_Magazine_Trn_MagazineDetail_and_History(trnMagazineDTO);

            return Json(insertDTO);
        }

        public async Task<IActionResult> TrackOut(StageLot stagelot)
        {
            stagelot.StatusCode = (int)StatusListEnum.LotComplete;
            var trackOutDetail = await _plasmaMagazineRepository.TrackOut(stagelot);
            return Json(trackOutDetail);
        }
        #endregion


        #region Magazine History
        public async Task<IActionResult> MagazineHistory(SearchData searchData)
        {
            ViewBag.SearchData = searchData.searchValue;
            ViewBag.StageValue = searchData.stagePlanSelect;

            if (!string.IsNullOrEmpty(searchData.searchValue))
            {
                var search = new SearchData
                {
                    searchValue = searchData.searchValue,
                    stagePlanSelect = searchData.stagePlanSelect
                };
                return View(search);
            }

            return View();
        }

        public async Task<IActionResult> _MagazineHistoryTable(SearchData searchData)
        {
            var magazineHistoryList = await _plasmaMagazineRepository.Get_Magazine_History(searchData);
            return PartialView(magazineHistoryList);
        }
        [HttpPost]
        public async Task<IActionResult> SearchList(SearchData searchData)
        {
            var removeSpace = !string.IsNullOrEmpty(searchData.searchValue) ? searchData.searchValue.Trim() : "";
      
            return RedirectToAction("MagazineHistory" , new { searchValue = removeSpace , 
                                                              stagePlanSelect = searchData.stagePlanSelect });
        }

        #endregion


        #region Magazine Maintenance
        public async Task<IActionResult> MagazineMaintenance(StageLot stagelot,
                                                             bool isExcelEmpty = true,
                                                             string Filename = "")
        {
            if (isExcelEmpty == false)
            {
                ModelState.AddModelError(string.Empty, "Import Excel");
               
            }

            if (!string.IsNullOrEmpty(Filename))
            {
                ViewBag.Success = $"{Filename} has been Succesfully Inserted";
            }
           
            //var magazineList = await _plasmaMagazineRepository.GetMagazineList(stagelot);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MagazineExcelInsert(IFormFile formFile)
        {
            if (formFile == null)
            {
                ModelState.AddModelError(string.Empty, "Import Excel");
                return RedirectToAction("MagazineMaintenance" , new { isExcelEmpty  = false });
            }

            var toList = _xMLConverter.ExcelFileToList(formFile);
            var toXML = _xMLConverter.ConvertToXml(toList);
            var insertXML = _plasmaMagazineRepository.Insert_XML_MS_Station_Magazine(toXML);
           
            return RedirectToAction("MagazineMaintenance", new {
                                                                  isExcelEmpty = true,
                                                                  Filename = formFile.FileName});
        }


        public async Task<ActionResult> DownloadFile()
        {
            var pathFile = "Magazine_Template.xlsx";

            var fileContent =await _downloadFile.Get_Template_For_Magazine(pathFile);

            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", pathFile);
        }
        #endregion


        #region Magazine Dashboard
        public async Task<IActionResult> MagazineDashboard()
        {
         
          return View();
        }


        public async Task<IActionResult> _MagazineDashboardListTable(StageLot stageLot)
        {
            var magazineList = await _plasmaMagazineRepository.Get_Magazine_DashBoard(stageLot);

            return PartialView(magazineList);
        }

        #endregion 

    }
}

