using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Common;
using BMS.Data;
using BMS.Data.Entities;
using BMS.Services.Users;
using BMS.ViewModels.Class;
using BMS.ViewModels.Common;
using BMS.ViewModels.User;

namespace BMS.Services.School
{
    public class SchoolService : ISchoolService
    {
        BMSContext bMSContext = new BMSContext();
        public SchoolService()
        {
        }
        public SchoolMaster GetSchoolById(int id)
        {
            return bMSContext.SchoolMaster.Where(m => m.SM_Id == id && m.IsActive==true).FirstOrDefault();
        }
        public SchoolMaster SaveSchool(SchoolVM model)
        {
            var entity = GetSchoolById(Convert.ToInt32(model.Id));
            if (entity == null)
            {
                entity = new SchoolMaster();
            }
            entity.SM_Name = model.Name;
            entity.SM_Address = model.Address;
            entity.SM_MobileNo = model.MobileNo;
            if (entity.SM_Id == 0)
            {
                entity.IsActive = true;
                bMSContext.SchoolMaster.Add(entity);
            }
            bMSContext.SaveChanges();
            return entity;
        }
        public List<DropdownVM> GetSchoolDropdown()
        {
            var list = bMSContext.SchoolMaster.Where(a=>a.IsActive==true)
                .Select(a => new DropdownVM()
                {
                    Text = a.SM_Name,
                    Value = a.SM_Id
                })
                .ToList();
            return list;
        }

        public PagedList<SchoolVM> GetSchools(SchoolSearchVM searchModel)
        {
            var schools = (from S in bMSContext.SchoolMaster
                           orderby S.SM_Id
                           where S.IsActive==true
                           select new SchoolVM()
                           {
                               Id = S.SM_Id,
                               Name = S.SM_Name,
                               Address = S.SM_Address,
                               MobileNo = S.SM_MobileNo,
                           });

            return new PagedList<SchoolVM>(schools, searchModel.PageIndex, searchModel.PageSize, new string[] { searchModel.SortDirection }, new string[] { searchModel.SortBy });
        }
        public bool DeleteSchool(int id)
        {
            var row = bMSContext.SchoolMaster.Where(a => a.SM_Id == id).FirstOrDefault();
            if (row != null)
            {
                row.IsActive = false;
                bMSContext.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
