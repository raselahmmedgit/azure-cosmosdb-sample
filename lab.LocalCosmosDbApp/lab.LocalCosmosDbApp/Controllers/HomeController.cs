using lab.LocalCosmosDbApp.Helpers;
using lab.LocalCosmosDbApp.Managers;
using lab.LocalCosmosDbApp.Models;
using lab.LocalCosmosDbApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using static lab.LocalCosmosDbApp.Utility.Enums;

namespace lab.LocalCosmosDbApp.Controllers
{
    public class HomeController : BaseController
    {
        #region Global Variable Declaration
        private readonly ILogger<HomeController> _logger;
        private static IEmailSenderManager _iEmailSenderManager;
        //IBusinessUnitToolInfoManager
        private readonly IBusinessUnitToolInfoManager _iBusinessUnitToolInfoManager;
        private readonly IAppDbInitManager _iAppDbInitManager;
        #endregion

        #region Constructor
        public HomeController(IEmailSenderManager iEmailSenderManager
            , IBusinessUnitToolInfoManager iBusinessUnitToolInfoManager
            , IAppDbInitManager iAppDbInitManager
            )
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            _logger = loggerFactory.CreateLogger<HomeController>();
            _iEmailSenderManager = iEmailSenderManager;
            _iBusinessUnitToolInfoManager = iBusinessUnitToolInfoManager;
            _iAppDbInitManager = iAppDbInitManager;
        }
        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            try
            {
                //var season = GetSeason(DateTime.Now);
                //var result = _iAppDbInitManager.InitDatabaseAndMasterDataAsync();
                return View();
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        public IActionResult ImportFiles()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportFiles(IFormFile uploadFile)
        {

            try
            {
                if (uploadFile == null)
                {
                    return ErrorNullView();
                }

                var result = await _iBusinessUnitToolInfoManager.UploadBusinessUnitToolInfosAsync(uploadFile);

                if (result.Success)
                {
                    this.FlashSuccess(MessageHelper.FileImported, "businessUnitToolInfoMessage");
                    return RedirectToAction("Index");
                }
                else
                {
                    this.FlashError(MessageHelper.FileParseFail, "businessUnitToolInfoMessage");
                    return View();
                }

            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }

        }

        [HttpPost]
        [Route("/Home/SendEmail")]
        public async Task<IActionResult> SendEmail(ContactUsModel model)
        {
            try
            {
                if (!CaptchaHelper.IsValidCaptcha(model.CaptchaToken, model.CaptchaId, model.CaptchaText))
                {
                    _result = Result.Fail(MessageHelper.CaptchaSecurityCode);
                    var jsonCaptcha = new { success = _result.Success, messagetype = _result.MessageType, message = _result.Message };
                    return new JsonResult(jsonCaptcha);
                }

                var emailSentResult = await _iEmailSenderManager.ContactSendEmailToAdmin(model);
                if (emailSentResult.Success)
                {
                    _result = Result.Ok(MessageHelper.SentMessage);
                }
                else
                {
                    _result = Result.Fail(MessageHelper.SentMessageFail);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LogMessageHelper.FormateMessageForException(ex, "SendEmail[POST]"));
                _result = Result.Fail(MessageHelper.SentMessageFail);
            }
            var json = new { success = _result.Success, messagetype = _result.MessageType, message = _result.Message };
            return new JsonResult(json);
        }

        [HttpGet]
        [Route("/Home/GetCaptcha")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = 0)]
        public IActionResult GetCaptcha()
        {
            try
            {
                var captchaData = CaptchaHelper.GenerateCaptcha();
                return new JsonResult(captchaData);
            }
            catch (Exception ex)
            {
                _logger.LogError(LogMessageHelper.FormateMessageForException(ex, "GetCaptcha[GET]"));
                return null;
            }
        }

        [Route("/robots.txt")]
        public string RobotsTxt()
        {
            var sb = new StringBuilder();
            sb.AppendLine("User-agent: *")
                .AppendLine("Disallow:")
                .Append("sitemap: ")
                .Append(this.Request.Scheme)
                .Append("://")
                .Append(this.Request.Host)
                .AppendLine("/sitemap.xml");

            return sb.ToString();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            try
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        public SeasonEnum GetSeason(DateTime date)
        {
            /* Astronomically Spring begins on March 21st, the 80th day of the year. 
             * Summer begins on the 172nd day, Fall, the 266th and Winter the 355th.
             * Of course, on a leap year add one day to each, 81, 173, 267 and 356.*/

            int doy = date.DayOfYear - Convert.ToInt32((DateTime.IsLeapYear(date.Year)) && date.DayOfYear > 59);

            if (doy < 80 || doy >= 355)
                return SeasonEnum.Winter;

            if (doy >= 80 && doy < 172) 
                return SeasonEnum.Spring;

            if (doy >= 172 && doy < 266) 
                return SeasonEnum.Summer;

            return SeasonEnum.Fall;
        }

        #endregion
    }
}
