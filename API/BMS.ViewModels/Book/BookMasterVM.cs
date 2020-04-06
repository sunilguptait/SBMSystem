using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BMS.ViewModels.Book
{
    public class BookMasterVM : BaseViewModel
    {
        public int Book_Id { get; set; }
        public string Book_Name { get; set; }
        public string Book_ShortName { get; set; }
        public int? Book_PublisherId { get; set; }
        public decimal? Book_Price { get; set; }
        public DateTime Book_CreationDate { get; set; }
        public int Book_CreatedBy { get; set; }
        public DateTime? Book_EditedDate { get; set; }
        public int? Book_EditedBy { get; set; }
        public int? Book_BSMId { get; set; }
        public int? Book_TypeId { get; set; }
        public string PublisherName { get; set; }
        public string BookType { get; set; }
        public int? DefaultQuantity { get; set; }
        public int? Quantity { get; set; }
        public string Book_Image { get; set; }//= "https://media.gettyimages.com/photos/stack-of-books-picture-id157482029?s=612x612";
        public bool? IsSelected { get; set; } = true;
        public int? Discount { get; set; }
        public decimal? TotalAmount { get; set; }
        public int RN { get; set; }
        public string Book_ImageFile { get; set; }
    }
    public class ClassBooksForStudentSearchModel
    {
        public int ClassId { get; set; }
        public int StudentId { get; set; }
    }
}
