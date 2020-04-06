using SBMS.Mobile.Models;
using SBMS.Mobile.Models.Common;
using SBMS.Mobile.Services.Caching;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using SBMS.Mobile.Models.Student;

namespace SBMS.Mobile.Services.Student
{
    public class StudentService : BaseService, IStudentService
    {
        public StudentService()
        {
        }
        public async Task<ApiBaseModel<StudentModel>> GetStudentById(int id)
        {
            var responce = await _ApiClient.Get<StudentModel>($"api/student/getbyid?id={id}");
            return responce;
        }
        public async Task<ApiBaseModel<string>> SaveStudent(StudentModel model)
        {
            var responce = await _ApiClient.Post<string>("api/student/create", model);
            return responce;
        }

        public async Task<ApiBaseModel<ObservableCollection<StudentModel>>> GetStudents(StudentSearchModel searchModel)
        {
            var responce = await _ApiClient.Post<ObservableCollection<StudentModel>>("api/student/list", searchModel);
            return responce;
        }
       
    }
}
