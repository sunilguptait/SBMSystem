using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Common;
using BMS.Data.Entities;
using BMS.ViewModels.BookSeller;
using BMS.ViewModels.Class;
using BMS.ViewModels.Common;
using BMS.ViewModels.User;

namespace BMS.Services.BookSeller
{
    public interface IBookSellerService
    {
        BookSellerMaster GetBookSellerById(int id);
        BookSellerMaster SaveBookSeller(BookSellerVM model);
        PagedList<BookSellerVM> GetBookSellers(BookSellerSearchVM searchModel);
        List<DropdownVM> GetBookSellerDropdown();

        #region Book Seller School Mapping
        Tuple<string, SellerSchoolMapping> SaveSellerSchoolMapping(SellerSchoolMappingVM model);
        PagedList<SellerSchoolMappingVM> GetSellerSchoolMapping(SellerSchoolMappingSearchVM searchModel);
        bool DeleteSellerSchoolMapping(int mappingId, int userId);
        #endregion
    }
}
