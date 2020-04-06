using BMS.Data.Entities;
using BMS.Services.School;
using BMS.Services.Student;
using BMS.ViewModels;
using BMS.ViewModels.Class;
using BMS.ViewModels.Student;
using BMS.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BMS.API.Controllers
{
    public class StudentController : BaseApiController
    {
        IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public ResponseModel<StudentMasterVM> GetById(int id)
        {
            var response = new ResponseModel<StudentMasterVM>();
            //var studentDetails = _studentService.GetStudentById(id);
            var studentDetails = _studentService.GetStudents(new StudentMasterSearchVM() { StudentId = id });
            if (studentDetails != null && studentDetails.Count>0)
            {
                response.Data = studentDetails.FirstOrDefault();
            }
            else
            {
                response.ErrorMessage = "Something went wrong. Please try again.";
            }
            return response;
        }

        [HttpPost]
        public ResponseModel<string> Create(StudentMasterVM model)
        {
            var response = new ResponseModel<string>();
            var studentDetails = _studentService.SaveStudent(model);
            if (studentDetails != null)
            {
                response.Data = model.Id > 0 ? "Student updated successfully" : "Student added successfully";
            }
            else
            {
                response.ErrorMessage = "Something went wrong. Please try again.";
            }
            return response;
        }

        [HttpPost]
        [HttpGet]
        public ResponseModel<List<StudentMasterVM>> List(StudentMasterSearchVM model)
        {
            var response = new ResponseModel<List<StudentMasterVM>>();
            var schoolsList = _studentService.GetStudents(model);
            response.Data = schoolsList;
            //response.TotalItems = schoolsList.ItemCount;
            return response;
        }

        [HttpPost]
        public ResponseModel<StudentMasterVM> BookSellerSaveStudent(StudentMasterVM model)
        {
            var response = new ResponseModel<StudentMasterVM>();
            var studentDetails = _studentService.BookSellerSaveStudent(model);
            if (string.IsNullOrEmpty(studentDetails.Item2))
            {
                var students = List(new StudentMasterSearchVM() { StudentId = studentDetails.Item1.St_id });
                response.Data = students.Data?.FirstOrDefault();
            }
            else
            {
                response.ErrorMessage = studentDetails.Item2;
            }
            return response;
        }

        [HttpPost]
        public ResponseModel<ImportStudentMainVM> ImportStudents(ImportStudentMainVM model)
        {
            var response = new ResponseModel<ImportStudentMainVM>();
            var responseData = _studentService.ImportStudents(model);
            response.ErrorMessage = responseData.Item2 ? "Some errors occure whole data importing, please resolve errors and try again.":"";
            response.Data = responseData.Item1;
            return response;
        }

        //[HttpGet]
        //public ResponseModel<List<DropdownVM>> GetSchoolDropdown()
        //{
        //    var response = new ResponseModel<List<DropdownVM>>();
        //    var PublishersList = _schoolService.GetSchoolDropdown();
        //    response.Data = PublishersList;
        //    return response;
        //}
    }
}
