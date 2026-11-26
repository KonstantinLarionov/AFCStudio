using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VDCompanyMVC.Models;

namespace VDCompanyMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PrivacyOperator _operator;
        private readonly FeedbackJournal _journal;

        public HomeController(IConfiguration configuration, ILogger<HomeController> logger, FeedbackJournal journal)
        {
            _logger = logger;
            _operator = new PrivacyOperator(configuration);
            _journal = journal;
        }

        public IActionResult Index()
        {
            ViewData["CanReceiveFeedback"] = _operator.HasConsentDocument && _journal.IsConfigured;
            return View(_operator);
        }

        [HttpGet("/privacy", Name = "PrivacyPolicy")]
        [HttpGet("/Home/PDN")]
        public IActionResult PDN() => View(_operator);

        [HttpGet("/consent", Name = "FeedbackConsent")]
        public IActionResult Consent() => View(_operator);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendFeedBack(FeedbackRequest request)
        {
            if (!_operator.HasConsentDocument || !_journal.IsConfigured)
                return StatusCode(503, new { message = "Форма временно недоступна." });

            if (!request.ConsentAccepted || request.ConsentVersion != _operator.ConsentVersion)
                return BadRequest(new { message = "Откройте актуальный текст согласия и подтвердите его. При необходимости обновите страницу." });

            if (!ModelState.IsValid)
                return BadRequest(new { message = "Проверьте имя, адрес электронной почты, тему и текст заявки." });

            try
            {
                var id = await _journal.RecordAsync(request, _operator.ConsentVersion);
                return Ok(new { id });
            }
            catch (Exception exception)
            {
                _logger.LogError("Не удалось записать заявку и согласие ({ErrorType}).", exception.GetType().Name);
                return StatusCode(503, new { message = "Не удалось отправить заявку. Попробуйте ещё раз позже." });
            }
        }
    }
}
