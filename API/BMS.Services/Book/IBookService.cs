using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Common;
using BMS.Data.Entities;
using BMS.ViewModels.Book;
using BMS.ViewModels.Class;
using BMS.ViewModels.Common;

namespace BMS.Services.Book
{
    public interface IBookService
    {
        BookMaster GetBookById(int id);
        BookMaster SaveBook(BookMasterVM model);
        PagedList<BookMasterVM> GetBooks(BookMasterSearchVM searchModel);
        List<DropdownVM> GetBookDropdown(int bookSellerId);
        #region Class Book Mapping
        Tuple<string, BooksClassMapping> SaveBooksClassMapping(BooksClassMappingVM model);
        PagedList<BooksClassMappingVM> GetBooksClassMapping(BooksClassMappingSearchVM searchModel);
        bool DeleteBookClassMapping(int mappingId, int userId);
        #endregion

        List<BookMasterVM> GetClassBooksForStudent(ClassBooksForStudentSearchModel searchModel);

    }
}
