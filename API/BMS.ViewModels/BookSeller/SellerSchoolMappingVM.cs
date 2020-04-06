using BMS.ViewModels.Common;
using System;

namespace BMS.ViewModels.BookSeller
{
    
    public class SellerSchoolMappingVM:BaseViewModel
    {
        public int SSM_Id { get; set; }
        public int SSM_BSMId { get; set; }
        public int SSM_SMId { get; set; }
        public bool SSM_IsDeleted { get; set; }
        public string BookSellerName { get; set; }
        public string SchoolName { get; set; }
    }

    public class SellerSchoolMappingSearchVM : BaseSearchModel
    {
    }
}
