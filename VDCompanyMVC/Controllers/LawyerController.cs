using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using VDCompanyMVC.Controllers.Core.LawyerCore;
using VDCompanyMVC.Models.DTO;
using VDCompanyMVC.Models.Entitys;
using VDCompanyMVC.Models.Objects;
using VDCompanyMVC.Models.Pages;
using VDCompanyMVC;

namespace VDCompany.Controllers
{
    public class LawyerController : Controller
    {
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

        private static readonly StartContext db = new StartContext(new DbContextOptions<StartContext>());
        private (string login, string password) userinfo = (null, null);
        private Lawyer curruser = null;
        #region AUTHLOGIN
        public bool Auth()
        {
            try
            {
                var login = HttpContext.Session.GetString("login");
                var password = HttpContext.Session.GetString("password");
                userinfo = (login, password);
                curruser = db.Lawyers.Where(f => f.Login == login && f.Password == password).FirstOrDefault();
                return db.Lawyers.Any(f => f.Login == login && f.Password == password);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        [HttpPost]
        public string GettingFiles(List<IFormFile> imgs, int id_case)
        {

            if (!Auth())
                return "";
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

        public IActionResult Cases()
        {
            if (!Auth())
                return RedirectToRoute(new { controller = "User", action = "Login" });
            var cas = db.LawyersCases.Where(f => f.LawyerId == curruser.Id).ToList();
            var allcases = new List<Case>();
            foreach (var item in cas)
            {
                var _case = db.Cases.Where(f => f.Id == item.CaseId).FirstOrDefault();
                if(_case != null)
                    allcases.Add(_case);
            }
            var model = new ModelLawyerCases
            {
                Cases = allcases
            };
            return View(model);
        }
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
        public IActionResult Report(int Id)
        {
            if (!Auth())
                return RedirectToRoute(new { controller = "User", action = "Login" });
            var model = new ModelLawyerReport();
            model.Id = Id;
            return View(model);
        }
        [HttpPost]
        public string Report(List<IFormFile> reps, int Id)
        {
            var mycase = db.Cases.Where(x => x.Id == Id).Include(x => x.Reports).FirstOrDefault();
            string name = $"Отчёт по заказу №{mycase.Id} от {DateTime.Now.ToShortDateString()}";
            foreach (var rep in reps)
            {
                string way = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\reports", name);
                var report = new Report{Name = name,CaseId= mycase.Id, Way = way, DateAdd = DateTime.Now, TypeReport = TypeReport.SEO};
                mycase.Reports.Add(report);
                using (var stream = new FileStream(way, FileMode.Create))
                {
                    rep.CopyTo(stream);
                }
            }
            db.SaveChanges();
            return JsonAnswer.L_Report(reps.Count.ToString());
            //return "{\"status\":\"success\", \"data\":\"Загружено файлов: " + reps.Count.ToString() + "\"}";
        }

        public IActionResult Index()
        {  
            if (!Auth())
                return RedirectToRoute(new { controller = "User", action = "Login" });
            return RedirectToRoute(new { controller = "Lawyer", action = "Cases" });
        }
        public IActionResult Contacts()
        {
            if (!Auth())
                return RedirectToRoute(new { controller = "User", action = "Login" });
            ServiceVDContacts contacts = db.Contacts.FirstOrDefault();
            return View(contacts);
        }
        public IActionResult PDN()
        {
            if (!Auth())
                return RedirectToRoute(new { controller = "User", action = "Login" });
            return View();
        }
    }
}
