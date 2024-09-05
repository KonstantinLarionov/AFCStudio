using AFCStudio.Models.Helpers;

using Microsoft.AspNetCore.Mvc;

using VDCompany.Controllers;

namespace VDCompanyMVC.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public string SendFeedBack( string name, string email, string message )
        {
            try
            {
                string content = "<p><font size = \"3\" face = \"Source Serif Pro\">Новое сообщение через обратную связь <br> <strong>Имя:</strong> " + name + " <br> <strong>Обратный Email:</strong> " + email + " <br> <strong>Сообщение:</strong><br> " + message + " </font></p>";
                Letters.Send( "afc.studio@yandex.ru", "Обратная связь.", content ).GetAwaiter().GetResult();
                return JsonAnswer.H_Success();
            }
            catch
            {
                return JsonAnswer.H_Error();
            }
        }
    }
}
