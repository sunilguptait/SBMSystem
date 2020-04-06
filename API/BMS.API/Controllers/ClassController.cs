using BMS.Services.Class;
using BMS.ViewModels;
using BMS.ViewModels.Class;
using BMS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BMS.API.Controllers
{
    public class ClassController : BaseApiController
    {
        IClassService _classService;
        public ClassController(IClassService bookService)
        {
            _classService = bookService;
        }

        [HttpPost]
        public ResponseModel<string> Create(ClassVM model)
        {
            var response = new ResponseModel<string>();
            var bookDetails = _classService.SaveClass(model);
            if (bookDetails != null)
            {
                response.Data = model.Id > 0 ? "Class updated successfully" : "Class created successfully";
            }
            else
            {
                response.ErrorMessage = "Something went wrong. Please try again.";
            }
            return response;
        }

        [HttpPost]
        public ResponseModel<List<ClassVM>> List(ClassSearchVM model)
        {
            var response = new ResponseModel<List<ClassVM>>();
            var booksList = _classService.GetClasses(model);
            response.Data = booksList.List;
            response.TotalItems = booksList.ItemCount;
            return response;
        }

        [HttpGet]
        public ResponseModel<List<DropdownVM>> GetClassDropdown()
        {
            var response = new ResponseModel<List<DropdownVM>>();
            var list = _classService.GetClassDropdown();
            response.Data = list;
            return response;
        }
    }
}
