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

namespace BMS.Services.School
{
    public interface ISchoolService
    {
        SchoolMaster GetSchoolById(int id);
        SchoolMaster SaveSchool(SchoolVM model);
        PagedList<SchoolVM> GetSchools(SchoolSearchVM searchModel);
        List<DropdownVM> GetSchoolDropdown();
        bool DeleteSchool(int id);
    }
}
