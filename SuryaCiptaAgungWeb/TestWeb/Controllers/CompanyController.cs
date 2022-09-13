using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Test.DataModels;
using Test.Repo;
using Test.ViewModels;

namespace TestWeb.Controllers
{
    public class CompanyController : Controller
    {
        private readonly SuryaCiptaAgungContext db;
        private readonly CompanyRepo repo;

        public CompanyController(SuryaCiptaAgungContext db)
        {
            this.db = db;
            repo = new CompanyRepo(db);
        }
        public IActionResult Index()
        {
            List<VMCompany> data = repo.alldata();
            return View(data);
        }
        public IActionResult Add()
        {
            string code = companycode();
            ViewBag.kode = code;
            return View();
        }
        [HttpPost]
        public JsonResult Add(VMCompany dataVM)
        {
            dataVM.CreatedBy = "Admin";

            bool respon = repo.add(dataVM);
            if (respon)
            {
                return Json(new {status = "success", data=dataVM.Code});
            }
            else
            {
                return Json(new { status = "unsuccess" });
            }
        }
        public IActionResult Edit(string code)
        {
            VMCompany dataVM = repo.getbyid(code);
            return View(dataVM);
        }

        public string companycode()
        {
            string nol = "0000";
            string companycode = "CP";
            TblMasterCompany data = db.TblMasterCompany.OrderByDescending(a => a.Code).FirstOrDefault();

            
            if(data != null)
            {
                string code = data.Code.Substring(2, 4);
                int tambah = int.Parse(code) + 1;
                nol += tambah;
                string kode = nol.Substring(tambah.ToString().Length, 4);
                companycode = data.Code.Substring(0, 2) + kode;
            }
            else
            {
                companycode = "CP0001";
            }

            return companycode;
        }
    }
}
