using SBMS.Mobile.Models;
using SBMS.Mobile.Models.Common;
using SBMS.Mobile.Models.Student;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SBMS.Mobile.Services.Student
{
    public interface IStudentService
    {
        Task<ApiBaseModel<StudentModel>> GetStudentById(int id);
        Task<ApiBaseModel<string>> SaveStudent(StudentModel model);
        Task<ApiBaseModel<ObservableCollection<StudentModel>>> GetStudents(StudentSearchModel searchModel);
    }
}
