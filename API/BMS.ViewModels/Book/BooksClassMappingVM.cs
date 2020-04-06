using BMS.ViewModels.Common;
using System;

namespace BMS.ViewModels.Book
{
    public class BooksClassMappingVM: BaseViewModel
    {
        public int BCM_Id { get; set; }
        public int BCM_BookId { get; set; }
        public int BCM_ClassId { get; set; }
        public bool? BCM_IsDeleted { get; set; }
        public bool? BCM_OutOfStock { get; set; }
        public string BookName { get; set; }
        public string ClassName { get; set; }
        public int? BCM_DefaultQty { get; set; }
    }

    public class BooksClassMappingSearchVM : BaseSearchModel
    {
    }
}
