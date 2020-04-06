using BMS.Services.School;
using BMS.ViewModels;
using BMS.ViewModels.Class;
using BMS.ViewModels.Common;
using BMS.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BMS.API.Controllers
{
    public class SchoolController : BaseApiController
    {
        ISchoolService _schoolService;
        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpPost]
        public ResponseModel<string> Create(SchoolVM model)
        {
            var response = new ResponseModel<string>();
            var schoolDetails = _schoolService.SaveSchool(model);
            if (schoolDetails != null)
            {
                response.Data = model.Id > 0 ? "School updated successfully" : "School created successfully";
            }
            else
            {
                response.ErrorMessage = "Something went wrong. Please try again.";
            }
            return response;
        }

        [HttpPost]
        public ResponseModel<List<SchoolVM>> List(SchoolSearchVM model)
        {
            var response = new ResponseModel<List<SchoolVM>>();
            var schoolsList = _schoolService.GetSchools(model);
            response.Data = schoolsList.List;
            response.TotalItems = schoolsList.ItemCount;
            return response;
        }

        [HttpGet]
        public ResponseModel<List<DropdownVM>> GetSchoolDropdown()
        {
            var response = new ResponseModel<List<DropdownVM>>();
            var PublishersList = _schoolService.GetSchoolDropdown();
            response.Data = PublishersList;
            return response;
        }
        [HttpGet]
        public ResponseModel<string> Delete(int id)
        {
            var response = new ResponseModel<string>();
            var deleteResponse = _schoolService.DeleteSchool(id);
            if (deleteResponse)
            {
                response.Data = "School deleted successfully.";
            }
            else
            {
                response.ErrorMessage = "Something went wrong. Please try again.";
            }
            return response;
        }
    }
}
