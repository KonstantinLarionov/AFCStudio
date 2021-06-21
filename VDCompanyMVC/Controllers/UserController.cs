using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Data.Entity.Validation;
using Microsoft.AspNetCore.Http;

using VDCompanyMVC.Models.DTO;
using VDCompanyMVC.Models.Pages;
using VDCompanyMVC.Models.Entitys;
using VDCompanyMVC.Models.Objects;
using VDCompanyMVC.Models.Secur;
using System.IO;
using VDCompanyMVC;
using AFCStudio.Models.Helpers;

namespace VDCompany.Controllers
{
    public class UserController : Controller
    {
        #region Dictionary
        Dictionary<string, TypeDoc> Keys = new Dictionary<string, TypeDoc>()
        {
            { "null", TypeDoc.NONE},
            { "png", TypeDoc.IMG},
            { "jpg", TypeDoc.IMG},
            { "jpeg", TypeDoc.IMG},
            { "bmp", TypeDoc.IMG},
            { "xlsx", TypeDoc.XLC},
            { "doc", TypeDoc.WORD},
            { "docs", TypeDoc.WORD},
            { "pdf", TypeDoc.PDF},
            { "mp3", TypeDoc.AUDIO},
            { "mp4", TypeDoc.VIDEO},
        };
        #endregion
        #region AUTHLOGIN
        private static readonly StartContext db = new StartContext(new DbContextOptions<StartContext>());
        private (string login, string password) userinfo = (null, null);
        private IQueryable curruser = null;
        public bool Auth()
        {
            try
            {
                var login = HttpContext.Session.GetString("login");
                var password = HttpContext.Session.GetString("password");
                userinfo = (login, password);
                curruser = db.Users.Where(f => f.Login == login && f.Password == password);
                return db.Users.Any(f => f.Email == login && f.Password == password);
            }
            catch
            {
                return false;
            }
        }
        public IActionResult Reg()
        {           
            return View();
        }
        public IActionResult Report(int Id)
        {
            if (!Auth())
                return RedirectToRoute(new { controller = "User", action = "Login" });
            var model = new ModelUserReport();
            model.Id = Id;
            return View(model);
        }
        public IActionResult Login()
        {     
            return View();
        }
        [HttpPost]
        public IActionResult Reg(string email, string name)
        {
            
            if (db.Users.Any(x => x.Email == email))
            {
                return View("Reg");
            }
            else
            {
                string referal = "";
                string psw = GenNewPsw(10);             
                do
                {
                    Random RNDREF = new Random();
                    referal = RNDREF.Next(1000000, 9999999).ToString();
                }
                while (db.Users.Any(x => x.RefCode == referal));
                db.Users.Add(new User
                { 
                    Email = email,
                    Login = email,
                    Name = name,
                    DateReg = DateTime.Now,
                    RefCode = $"referal:{ referal }",
                    Password = psw
                });
                db.SaveChanges();
                string content = "<p><strong><font size=\"3\" face=\"Source Serif Pro\">Добро пожаловать в AFCStudio " + name + "!</font></strong></p><p><font size = \"3\" face = \"Source Serif Pro\">Спасибо за регистрацию на нашем сайте</font></p><p><font size = \"3\" face = \"Source Serif Pro\"> Ваш логин: " + email + " </font ></p><p><font size = \"3\" face = \"Source Serif Pro\" > Ваш пароль: " + psw + " </font></p>";
                Letters.Send(email, "Регистрация на сайте.", content).GetAwaiter().GetResult();

                return Redirect("Login");
            }
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {           
            User user = null;
            Admin admin = null;
            Lawyer lawyer = null;

            user = db.Users.Where(u => u.Login == email && u.Password == password).FirstOrDefault();
            if (user != null)
            {
                user.LASTLOGIN = DateTime.Now;
                db.SaveChanges();
                HttpContext.Session.SetString("login", email);
                HttpContext.Session.SetString("password", password);
                HttpContext.Response.Cookies.Append("login", email);
                HttpContext.Response.Cookies.Append("password", password);
                return RedirectToAction("Cases", "User", new { notify = 1 });
                //return RedirectToRoute(new { controller = "User", action = "Cases" }); 
            }
            else 
            {
                lawyer = db.Lawyers.Where(u => u.Login == email && u.Password == password).FirstOrDefault();
                if (lawyer != null)
                {
                    HttpContext.Session.SetString("login", email);
                    HttpContext.Session.SetString("password", password);
                    HttpContext.Response.Cookies.Append("login", email);
                    HttpContext.Response.Cookies.Append("password", password);
                    return RedirectToRoute(new { controller = "Lawyer", action = "Index" });
                }
                else 
                {
                    admin = db.Admins.Where(u => u.Login == email && u.Password == password).FirstOrDefault();
                    if (admin != null)
                    {
                        HttpContext.Session.SetString("login", email);
                        HttpContext.Session.SetString("password", password);
                        HttpContext.Response.Cookies.Append("login", email);
                        HttpContext.Response.Cookies.Append("password", password);
                        return RedirectToRoute(new { controller = "Admin", action = "Index" });
                    }
                    else 
                    {
                        return RedirectToRoute(new { controller = "User", action = "Login" });
                    }
                }
            }            
        }
        [HttpPost]
        public string Reset([FromBody] RegDTO userDTO)
        {
            if (db.Users.Any(x => x.Email == userDTO.Email))
            {
                var user = db.Users.Where(x => x.Email == userDTO.Email).FirstOrDefault();
                //Mailler.SendEmailAsync(userDTO.Email, "VDCOMPANY", "Забыли пароль?", "Ваш пароль на сервисе VDCompany: <strong>" + user.Password + "</strong>").GetAwaiter().GetResult();
                string content = "<p><font size = \"3\" face = \"Source Serif Pro\">Забыли пароль?<br>Ваш пароль на сайте AFCStudio: <strong>" + user.Password + "</strong></font></p>";
                Letters.Send(userDTO.Email, "Востановление пароля", content).GetAwaiter().GetResult();
                return "Пароль успешно восстановлен. Проверьте вашу почту";
            }
            else
            {
                return "Данный Email не был зарегистрирован ранее. Пройдите процедуру регистрации.";
            }
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToRoute(new { controller = "User", action = "Login" });
        }
        #endregion
        [HttpGet]
        public IActionResult Index()
        {
            if(!Auth())
                return RedirectToRoute(new { controller = "User", action = "Login" });
            
            return RedirectToRoute(new {controller = "User", action = "Cases"} );
        }

        [HttpPost]
        public string GettingFiles(List<IFormFile> imgs, int id_case)
        {

            if (!Auth())
                return JsonAnswer.U_Unauthorized();
            try
            {
                List<string> fileName = new List<string>();
                List<Doc> files = new List<Doc>();
                var @case = db.Cases.Where(x => x.Id == id_case).Include(x => x.Docs).FirstOrDefault();
                string format;
                foreach (var file in imgs)
                {
                    string path1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/received_files/", userinfo.login);
                    bool folder = System.IO.Directory.Exists(path1);
                    DirectoryInfo dirInfo = new DirectoryInfo(path1);
                    if (!dirInfo.Exists)
                    {
                        dirInfo.Create();
                    }
                    string filename = file.FileName;
                    string[] words = filename.Split(new char[] { '.' });
                    format = words[1];
                    var forma = Keys["null"];
                    if (Keys.ContainsKey(format)) forma = Keys[format];
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/received_files/", userinfo.login, file.FileName);
                    bool fileExist = System.IO.File.Exists(path);
                    if (fileExist == true)
                    {
                        string nfile = words[0] + "(1)." + words[1];
                        string nfilename = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/received_files/", userinfo.login, nfile);
                        path = nfilename;
                    }
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    files.Add(new Doc()
                    {
                        URL = path,
                        Name = file.FileName,
                        DateAdd = DateTime.Now,
                        Type = forma,
                    });
                    @case.Docs.Add(new Doc()
                    {
                        URL = path,
                        Name = file.FileName,
                        DateAdd = DateTime.Now,
                        Type = forma,
                    });

                }
                db.SaveChanges();
                string tojsn = files.ToJson();
                string jret = "{\"status\":\"success\", \"data\":\"Загружено файлов: " + imgs.Count.ToString() + "\", \"files\":" + tojsn + "}";
                return jret;
            }
            catch (Exception exp)
            {
                return "{\"status\":\"error\", \"data\": \"" + exp.ToString() + "\"}";
            }
        }

        [HttpGet]
        public IActionResult Contacts()
        {
            if (!Auth())
                return RedirectToRoute(new { controller = "User", action = "Login" });
            ServiceVDContacts contacts = db.Contacts.FirstOrDefault();
            return View(contacts);
        }
        [HttpGet]
        public IActionResult PDN()
        {
            if (!Auth())
                return RedirectToRoute(new { controller = "User", action = "Login" });
            return View();
        }
        [HttpGet]
        public IActionResult Case(int Id)
        {

            if (!Auth())
                return RedirectToRoute(new { controller = "User", action = "Login" });
            var _case = db.Cases.Where(f => f.Id == Id).Include(d => d.Docs).FirstOrDefault();
            var user = db.Users.Where(f => f.Login == userinfo.login && f.Password == userinfo.password).FirstOrDefault();
            var index_lawyers = db.LawyersCases.Where(f => f.CaseId == Id).ToList();
            var lawyers_in_case = new List<Lawyer>();
            foreach (var item in index_lawyers)
            {
                var l = db.Lawyers.Where(f => f.Id == item.LawyerId).FirstOrDefault();
                lawyers_in_case.Add(l);
            }
            MyCaseDTO myCaseDTO = new MyCaseDTO
                (
                    _case,
                    user
                );
            myCaseDTO.Lawyers = lawyers_in_case;
            return View(myCaseDTO);
        }
        #region BILLS
        [HttpGet]
        public IActionResult Bills()
        {
            if (!Auth())
                return RedirectToRoute(new { controller = "User", action = "Login" });
            var userwithbills = db.Users.Where(f => f.Login == userinfo.login && f.Password == userinfo.password).Include(f => f.Bills).FirstOrDefault();
            userwithbills.Bills.ForEach(x => x.IsRead = true);
            db.SaveChanges();
            var model = new ModelUserBills
            {
                Bills = userwithbills.Bills
            };
            return View(model);
        }
        #endregion
        #region CASES
        [HttpGet]
        public IActionResult CreateCase()
        {
            if (!Auth())
                return RedirectToRoute(new { controller = "User", action = "Login" });
            return View();
        }
        [HttpPost]
        public string CreateCase([FromBody] CaseDTO newcase)
        {
            if (!Auth())
                return JsonAnswer.U_Unauthorized();
            var userwithcases = db.Users.Where(f => f.Login == userinfo.login && f.Password == userinfo.password).Include(x => x.Cases).FirstOrDefault();
            Case new_case = new Case();
            try
            {
                new_case.Name = newcase.Name;
                new_case.Type = newcase.Type;
                new_case.Dialog = new Dialog() { DateCreate = DateTime.Now, Admins = new List<Admin>(), Lawyers = new List<Lawyer>(), Messages = new List<Message>(), Users = new List<User>() { userwithcases } };
                new_case.Description = newcase.Description;
                new_case.DateStart = DateTime.Now;
                userwithcases.Cases.Add(new_case);
                db.SaveChanges();
            }
            catch
            {
                return JsonAnswer.U_Error();
            }
            try
            {
                string content = "<p><font size =\"3\" face=\"Source Serif Pro\">Вы заказали новую услугу на сервисе AFCStudio!</font></p><p><font size =\"3\" face=\"Source Serif Pro\">Наименование вашей услуги: "+ new_case.Name + "</font></p><p><font size =\"3\" face=\"Source Serif Pro\">Тип вашей услуги: " + new_case.Type + "</font></p><p><font size = \"3\" face = \"Source Serif Pro\">Дата создания: " + new_case.DateStart + "</font></p><p><font size = \"3\" face = \"Source Serif Pro\"> После регистрации услуга появится в вашем личном кабинете в списке услуг и вам будет назначен подходящий специалист. </font ></p>";
                Letters.Send(userinfo.login, "Создание новой услуги.", content).GetAwaiter().GetResult();              
            }
            catch 
            {
            
            }
            return JsonAnswer.U_Success();
        }
        [HttpGet]
        public IActionResult Cases(string notify = null)
        {
            if (!Auth())
                return RedirectToRoute(new { controller = "User", action = "Login" });
            var userwithcases = db.Users.Where(f => f.Email == userinfo.login && f.Password == userinfo.password).Include(x => x.Cases).FirstOrDefault();
            var model = new ModelUserCases
            {
                Cases = userwithcases.Cases
            };
            ViewData["notify"] = notify;
            return View(model);
        }
        #endregion
        /*[HttpPost(Name = "ChangeStatus")]
        public object[] ChangeStatus([FromBody] ChangerBillDTO CBD )
        {
            return new object[] { HttpContext.SendToUser(u => u.ChangeStateBill(CBD.Id)), CBD.Id };
        }
        [HttpPost(Name = "GetCases")]
        public List<Models.Objects.Case> GetCases()
        {
            return HttpContext.SendToUser(u => u.GetCases());
        }
        [HttpPost(Name = "GetLawyers")]
        public List<Models.Objects.Lawyer> GetLawyers()
        {
            return HttpContext.SendToUser(u => u.GetLawyers());
        }*/
        #region Helpers
        private string GetHash(string data, int length = 0)
        {
            var tmpSource = System.Text.ASCIIEncoding.ASCII.GetBytes(data);
            byte[] tmpNewHash = null;
            string h = "";
            using (var crypto = new System.Security.Cryptography.SHA256CryptoServiceProvider())
            {
                tmpNewHash = crypto.ComputeHash(tmpSource);
            }
            for (int i = 0; i < tmpNewHash.Length; i++)
            {
                h += tmpNewHash[i].ToString("X2");
            }
            return length > 0 ? h.Remove(length) : h;
        }
        private string GenNewPsw(int length, string s = "zaq1xsw2cde3vfr4bgt5nhy6mju7ki8lo9p0")
        {
            string g = "";
            for (int i = 0; i < length; i++)
            {
                g += s[new Random().Next(s.Length)];
            }
            return g;
        }
        #endregion

        [HttpGet]
        public string CheckingAccount()
        {
            if (!Auth())
                return JsonAnswer.U_Unauthorized();
            var usercheckingaccount = db.Users.Where(x => x.Email == userinfo.login && x.Password == userinfo.password).Include(x => x.Bills).FirstOrDefault();
            if (usercheckingaccount != null) 
            {
                var bill = usercheckingaccount.Bills;
                if (bill.Any(x => x.IsRead == false))
                {
                    return "{\"status\":\"success\"}";
                }
            }           
            return "{\"status\":\"error\"}";
        }
    }
}
