using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WEBMES_V2.Models.DomainModels.MaterialStaging;
using WEBMES_V2.Models.DTO.MaterialStagingDTO;
using WEBMES_V2.Models.ISQLRepository;
using WEBMES_V2.Models.StaticModels.Generic;
using NiceLabel.SDK;


namespace WEBMES_V2.Controllers
{
    [Authorize(Policy = "UserCred")]
    public class MaterialStagingController : Controller
    {
        private readonly IMaterialRepository _materialRepository;
        private IXMLConverter _xmlConverter;
        private readonly IDownloadFile _downloadFile;



        public MaterialStagingController(IMaterialRepository materialRepository,
                                  IXMLConverter xMLConverter, 
                                  IDownloadFile downloadFile)
        {
            _materialRepository = materialRepository;
            _xmlConverter = xMLConverter;
            _downloadFile = downloadFile;
        }

        #region Material Staging

        public IActionResult MaterialStagingView()
        {
            return View();
        }








        public IActionResult MaterialView()
        {
            return View();
        }

        public async Task<IActionResult> CheckSidAvailable(StageLot stageLot)
        {
            var isExist = await _materialRepository
                                        .CheckSidAvailable(stageLot);

            if (!isExist)
            {
                return Json(new
                {
                    status = 2,
                    details = isExist,
                    message = $"SID not available in SID Matrix, Please encode first before proceeding."
                });
            }

            //Check if SID is already Track IN
            var isThawIn = await _materialRepository
                                         .CheckSidInTracking(stageLot);

            if (!isThawIn)
            {
                //Get Thawing Time and WorkLife
                var sidDetails = await _materialRepository
                                        .GetMaterialThawing(stageLot);

                var thawingTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                var thawedTime = DateTime.Now.AddHours((int)sidDetails.ThawingTime).ToString("MM/dd/yyyy HH:mm:ss");
                var workLife = DateTime.Now.AddHours((int)sidDetails.WorkLife).ToString("MM/dd/yyyy HH:mm:ss");

                return Json(new
                {
                    status = 1,
                    details = new
                    {
                        isExist,
                        thawingTime,
                        thawedTime,
                        workLife
                    },
                    message = $"SID Available"
                });
            } 

            if (isThawIn)
            {
                var sidDetails = await _materialRepository
                                        .GetMaterialThawingTracking(stageLot);

                if (sidDetails.ThawOut == null)
                {
                    var ThawIn = sidDetails.ThawIn.ToString();
                    var ThawOutEnd = sidDetails.ThawOutEnd.ToString();
                    var WorkLifeEnd = sidDetails.WorkLifeEnd.ToString();

                    return Json(new
                    {
                        status = 3,
                        details = new { 
                            isExist,
                            ThawIn,
                            ThawOutEnd,
                            WorkLifeEnd
                        },
                        batchID = sidDetails.Id,
                        message = ""
                    });
                }
            }

            return Json(new 
            { 
                status = 1,
                details = isExist, 
                message = $"SID Available"
            });
        }

        public async Task<IActionResult> InsertThawingDetails(StageLot stageLot)
        {
            var insertId = new MtlMaterialTracking();
            var insertBatchDetails = new MtlMaterialTracking
            {
                Qrcode = stageLot.QRCode,
                Sid = stageLot.SID,
                MaterialDesc = stageLot.MaterialDesc,
                Batch = stageLot.Batch,
                BatchNumber = stageLot.BatchNumber,
                ThawIn = stageLot.ThawIn,
                ThawInBy = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                ThawOut = stageLot.ThawOut,
                ThawOutEnd = stageLot.ThawOutEnd,
                WorkLifeEnd = stageLot.WorkLifeEnd,
                ExpirationDate = stageLot.ExpirationDate,
                MaterialType = stageLot.MaterialType             
            };

            insertId = await _materialRepository.Insert_MTL_Material_Tracking(insertBatchDetails);

            return Json(new
            {
                status = 4,
                message = $"Succesful saving, Thawing start."
            });
        }

        public async Task<IActionResult> ThawOut(StageLot stageLot)
        {
            var isUpdated = true;


            return Json(new
            {
                status = 5,
                message = $""
            });
        }


        #endregion

        #region Material Dashboard

        public async Task<IActionResult> MaterialDashboard()
        {
            return View();
        }

        public async Task<IActionResult> _MaterialDashboardListTable(StageLot stageLot)
        {
            var materialList = await _materialRepository.Get_Material_Dashboard(stageLot);
            return PartialView(materialList);
        }


        #endregion

        #region Material History

        public async Task<IActionResult> MaterialHistory(string searchValue)
        {
            ViewBag.SearchValue = searchValue;

            if (string.IsNullOrEmpty(searchValue))
            {
                var search = new SearchData
                {
                    searchValue = searchValue
                };
                return View(search);
            }
            return View();
        }

        //public async Task<IActionResult> _MaterialHistoryTable(SearchData searchData)
        //{
        //    var materialHistoryList = await _materialRepository
        //    return PartialView();
        //}

        [HttpPost]
        public async Task<IActionResult> SearchList(SearchData searchData)
        {
            var removeSpace = !string.IsNullOrEmpty(searchData.searchValue) ? searchData.searchValue.Trim() : "";
            return RedirectToAction("MaterialHistory", new { searchValue = removeSpace });
        }

        #endregion

    }
}
