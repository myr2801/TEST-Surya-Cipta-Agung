using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.DataModels;
using Test.ViewModels;

namespace Test.Repo
{
    public class CompanyRepo
    {
        private readonly SuryaCiptaAgungContext db;

        public CompanyRepo(SuryaCiptaAgungContext db)
        {
            this.db = db;
        }

        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TblMasterCompany, VMCompany>();
                cfg.CreateMap<VMCompany, TblMasterCompany>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        public List<VMCompany> alldata()
        {
            List<VMCompany> dataVM = new List<VMCompany>();

            List<TblMasterCompany> dataModel = db.TblMasterCompany.Where(a=>a.IsDelete == false).ToList();

            dataVM = GetMapper().Map<List<VMCompany>>(dataModel);

            return dataVM;
        }
        
        public bool add(VMCompany dataVM)
        {
            bool issucces = false;
            TblMasterCompany data = new TblMasterCompany();

            try
            {
                data.Code = dataVM.Code;
                data.Email = dataVM.Email;
                data.Phone = dataVM.Phone;
                data.Name = dataVM.Name;
                data.Address = dataVM.Address;
                data.CreatedBy = dataVM.CreatedBy;
                data.CreatedDate = DateTime.Now;
                data.IsDelete = false;
                db.Add(data);
                db.SaveChanges();

                issucces = true;
            }
            catch
            {
                throw;
            }
            return issucces;
        }
        public VMCompany getbyid(string code)
        {
            VMCompany dataVM = new VMCompany();

            TblMasterCompany dataModel = db.TblMasterCompany.Where(a => a.Code == code).FirstOrDefault();

            dataVM = GetMapper().Map<VMCompany>(dataModel);
            return dataVM;
        }

    }
}
