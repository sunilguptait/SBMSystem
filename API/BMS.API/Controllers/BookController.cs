using BMS.Common;
using BMS.Services.Book;
using BMS.ViewModels;
using BMS.ViewModels.Book;
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
    public class BookController : BaseApiController
    {
        IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public ResponseModel<string> Create(BookMasterVM model)
        {
            var response = new ResponseModel<string>();
            var bookDetails = _bookService.SaveBook(model);
            if (bookDetails != null)
            {
                response.Data = model.Book_Id > 0 ? "Book updated successfully" : "Book created successfully";
            }
            else
            {
                response.ErrorMessage = "Something went wrong. Please try again.";
            }
            return response;
        }

        [HttpPost]
        public ResponseModel<List<BookMasterVM>> List(BookMasterSearchVM model)
        {
            var response = new ResponseModel<List<BookMasterVM>>();
            var booksList = _bookService.GetBooks(model);
            booksList.List.ToList().ForEach(a=>a.Book_Image=a.Book_Image.AddBaseURL());
            response.Data = booksList.List;
            response.TotalItems = booksList.ItemCount;
            return response;
        }

        [HttpGet]
        public ResponseModel<List<DropdownVM>> GetBookDropdown()
        {
            var response = new ResponseModel<List<DropdownVM>>();
            var list = _bookService.GetBookDropdown(SessionManager.UserId);
            response.Data = list;
            return response;
        }

        #region Class Book Mapping
        [HttpPost]
        public ResponseModel<string> CreateBookClassMapping(BooksClassMappingVM model)
        {
            var response = new ResponseModel<string>();
            var mappingDetails = _bookService.SaveBooksClassMapping(model);
            if (string.IsNullOrEmpty(mappingDetails.Item1))
            {
                response.Data = "Book class mapping saved successfully";
            }
            response.ErrorMessage = mappingDetails.Item1;
            return response;
        }

        [HttpPost]
        public ResponseModel<List<BooksClassMappingVM>> GetBookClassMappingList(BooksClassMappingSearchVM model)
        {
            var response = new ResponseModel<List<BooksClassMappingVM>>();
            var mappingList = _bookService.GetBooksClassMapping(model);
            response.Data = mappingList.List;
            response.TotalItems = mappingList.ItemCount;
            return response;
        }
        [HttpGet]
        public ResponseModel<string> DeleteBookClassMapping(int mappingId)
        {
            var response = new ResponseModel<string>();
            var deleteResponse = _bookService.DeleteBookClassMapping(mappingId, SessionManager.UserId);
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

        [HttpPost]
        public ResponseModel<List<BookMasterVM>> GetClassBooksForStudent(ClassBooksForStudentSearchModel searchModel)
        {
            var response = new ResponseModel<List<BookMasterVM>>();
            var booksList = _bookService.GetClassBooksForStudent(searchModel);
            response.Data = booksList;
            return response;
        }

    }
}
