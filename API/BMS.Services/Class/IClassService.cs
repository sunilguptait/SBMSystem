using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Common;
using BMS.Data.Entities;
using BMS.ViewModels.Class;
using BMS.ViewModels.Common;
using BMS.ViewModels.User;

namespace BMS.Services.Class
{
    public interface IClassService
    {
        ClassMaster GetClassById(int id);
        ClassMaster SaveClass(ClassVM model);
        PagedList<ClassVM> GetClasses(ClassSearchVM searchModel);
        List<DropdownVM> GetClassDropdown();
    }
}
