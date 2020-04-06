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

namespace BMS.Services.Class
{
    public class ClassService : IClassService
    {
        BMSContext bMSContext = new BMSContext();

        public ClassMaster GetClassById(int id)
        {
            return bMSContext.ClassMaster.Where(m => m.Class_Id == id).FirstOrDefault();
        }
        public ClassMaster SaveClass(ClassVM model)
        {
            var entity = GetClassById(Convert.ToInt32(model.Id));
            if (entity == null)
            {
                entity = new ClassMaster();
            }
            entity.Class_Name = model.Name;
            entity.Class_ShortName = model.ShortName;
            if (entity.Class_Id == 0)
            {
                bMSContext.ClassMaster.Add(entity);
            }
            bMSContext.SaveChanges();
            return entity;
        }
        public PagedList<ClassVM> GetClasses(ClassSearchVM searchModel)
        {
            var classes = (from S in bMSContext.ClassMaster
                           orderby S.Class_Id
                           select new ClassVM()
                           {
                               Id = S.Class_Id,
                               Name = S.Class_Name,
                               ShortName = S.Class_ShortName,
                           });

            return new PagedList<ClassVM>(classes, searchModel.PageIndex, searchModel.PageSize, new string[] { searchModel.SortDirection }, new string[] { searchModel.SortBy });
        }

        public List<DropdownVM> GetClassDropdown()
        {
            var list = bMSContext.ClassMaster
                .Select(a => new DropdownVM()
                {
                    Text = a.Class_Name,
                    Value = a.Class_Id
                })
                .ToList();
            return list;
        }
    }
}
