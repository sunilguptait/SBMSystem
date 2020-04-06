using BMS.Data.Entities;
using BMS.Services.BookSeller;
using BMS.Services.Users;
using BMS.ViewModels;
using BMS.ViewModels.BookSeller;
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
    public class BookSellerController : BaseApiController
    {
        IBookSellerService _bookSellerService;
        public BookSellerController(IBookSellerService bookSellerService)
        {
            _bookSellerService = bookSellerService;
        }

        [HttpGet]
        public ResponseModel<BookSellerMaster> GetById(int id)
        {
            var response = new ResponseModel<BookSellerMaster>();
            var bookSellerDetails = _bookSellerService.GetBookSellerById(id);
            if (bookSellerDetails != null)
            {
                response.Data = bookSellerDetails;
            }
            else
            {
                response.ErrorMessage = "Something went wrong. Please try again.";
            }
            return response;
        }

        [HttpPost]
        public ResponseModel<string> Create(BookSellerVM model)
        {
            var response = new ResponseModel<string>();
            var bookSellerDetails = _bookSellerService.SaveBookSeller(model);
            if (bookSellerDetails != null)
            {
                response.Data = model.Id > 0 ? "Book seller updated successfully" : "Book seller created successfully";
            }
            else
            {
                response.ErrorMessage = "Something went wrong. Please try again.";
            }
            return response;
        }

        [HttpPost]
        public ResponseModel<List<BookSellerVM>> List(BookSellerSearchVM model)
        {
            var response = new ResponseModel<List<BookSellerVM>>();
            var bookSellersList = _bookSellerService.GetBookSellers(model);
            response.Data = bookSellersList.List;
            response.TotalItems = bookSellersList.ItemCount;

            return response;
        }

        [HttpGet]
        public ResponseModel<List<DropdownVM>> GetBookSellerDropdown()
        {
            var response = new ResponseModel<List<DropdownVM>>();
            var PublishersList = _bookSellerService.GetBookSellerDropdown();
            response.Data = PublishersList;
            return response;
        }

        #region Book Seller School Mapping
        [HttpPost]
        public ResponseModel<string> CreateBookSellerSchoolMapping(SellerSchoolMappingVM model)
        {
            var response = new ResponseModel<string>();
            var mappingDetails = _bookSellerService.SaveSellerSchoolMapping(model);
            if (string.IsNullOrEmpty(mappingDetails.Item1))
            {
                response.Data = "Seller school mapping saved successfully";
            }
            response.ErrorMessage = mappingDetails.Item1;
            return response;
        }

        [HttpPost]
        public ResponseModel<List<SellerSchoolMappingVM>> GetBookSellerSchoolMappingList(SellerSchoolMappingSearchVM model)
        {
            var response = new ResponseModel<List<SellerSchoolMappingVM>>();
            var mappingList = _bookSellerService.GetSellerSchoolMapping(model);
            response.Data = mappingList.List;
            response.TotalItems = mappingList.ItemCount;
            return response;
        }
        [HttpGet]
        public ResponseModel<string> DeleteBookSellerSchool(int mappingId)
        {
            var response = new ResponseModel<string>();
            var deleteResponse = _bookSellerService.DeleteSellerSchoolMapping(mappingId, SessionManager.UserId);
            if (deleteResponse)
            {
                response.Data = "Mapping deleted successfully.";
            }
            else
            {
                response.ErrorMessage = "Something went wrong. Please try again.";
            }
            return response;
        }
        #endregion
    }
}
