using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Common;
using BMS.Data.Entities;
using BMS.ViewModels.Student;

namespace BMS.Services.Student
{
    public interface IStudentService
    {
        StudentMaster GetStudentById(int id);
        StudentMaster SaveStudent(StudentMasterVM model);
        StudentEnrollment SaveStudentEnrollment(StudentMasterVM model);
        List<StudentMasterVM> GetStudents(StudentMasterSearchVM searchModel);
        PagedList<StudentMasterVM> GetStudentsByLinq(StudentMasterSearchVM searchModel);
        Tuple<StudentMaster, string> BookSellerSaveStudent(StudentMasterVM model);
        Tuple<ImportStudentMainVM, bool> ImportStudents(ImportStudentMainVM model);
    }
}
