using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Common;
using BMS.Data;
using BMS.Data.Entities;
using BMS.ViewModels.Book;
using BMS.ViewModels.Class;
using BMS.ViewModels.Common;

namespace BMS.Services.Book
{
    public class BookService : IBookService
    {
        BMSContext bMSContext = new BMSContext();

        public BookMaster GetBookById(int id)
        {
            return bMSContext.BookMaster.Where(m => m.Book_Id == id).FirstOrDefault();
        }
        public BookMaster SaveBook(BookMasterVM model)
        {
           
            var entity = GetBookById(Convert.ToInt32(model.Book_Id));
            if (entity == null)
            {
                entity = new BookMaster();
                entity.Book_CreationDate = System.DateTime.Now;

            }
            entity.Book_Id = model.Book_Id;
            entity.Book_Name = model.Book_Name;
            entity.Book_ShortName = model.Book_ShortName;
            entity.Book_PublisherId = model.Book_PublisherId;
            entity.Book_Price = Convert.ToDecimal(model.Book_Price);
            entity.Book_CreatedBy = model.Book_CreatedBy;
            entity.Book_EditedDate = System.DateTime.Now;
            entity.Book_EditedBy = model.Book_EditedBy;
            entity.Book_BSMId = model.UserId;
            entity.Book_TypeId = model.Book_TypeId;
            //SAVE IMAGE
            if (!string.IsNullOrEmpty(model.Book_ImageFile) && model.Book_ImageFile.Split(',').Length>1)
            {
                //string imagePath = CommonMethods.SaveFile("/Content/Images/Book", model.Book_ImageFile);
                string imagePath = CommonMethods.SaveFileFromBase64String("/Content/Images/Book", model.Book_ImageFile.Split(',')[1]);
                entity.Book_Image = imagePath;
            }
            //
            if (entity.Book_Id == 0)
            {
                bMSContext.BookMaster.Add(entity);
            }
            bMSContext.SaveChanges();
            return entity;
        }
        public PagedList<BookMasterVM> GetBooks(BookMasterSearchVM searchModel)
        {
            var books = (from BSM in bMSContext.BookMaster
                         join S in bMSContext.PublisherMaster on BSM.Book_PublisherId equals S.Publisher_id
                         join BTM in bMSContext.BookTypeMaster on BSM.Book_TypeId equals BTM.BT_Id
                         orderby BSM.Book_Id
                         where BSM.Book_BSMId == searchModel.UserId
                         select new BookMasterVM()
                         {
                             Book_Id = BSM.Book_Id,
                             Book_Name = BSM.Book_Name,
                             Book_ShortName = BSM.Book_ShortName,
                             Book_PublisherId = BSM.Book_PublisherId,
                             Book_Price = BSM.Book_Price,
                             Book_CreationDate = BSM.Book_CreationDate,
                             Book_CreatedBy = BSM.Book_CreatedBy,
                             Book_EditedDate = BSM.Book_EditedDate,
                             Book_EditedBy = BSM.Book_EditedBy,
                             PublisherName = S.Publisher_Name,
                             Book_Image = BSM.Book_Image,
                             BookType = BTM.BT_Name,
                             Book_TypeId=BSM.Book_TypeId
                         });
            return new PagedList<BookMasterVM>(books, searchModel.PageIndex, searchModel.PageSize, new string[] { searchModel.SortDirection }, new string[] { searchModel.SortBy });
        }
        public List<DropdownVM> GetBookDropdown(int bookSellerId)
        {
            var list = bMSContext.BookMaster.Where(a => a.Book_BSMId == bookSellerId)
                .Select(a => new DropdownVM()
                {
                    Text = a.Book_Name,
                    Value = a.Book_Id
                })
                .ToList();
            return list;
        }

        #region Class Book Mapping
        public Tuple<string, BooksClassMapping> SaveBooksClassMapping(BooksClassMappingVM model)
        {
            var existingRow = bMSContext.BooksClassMapping.Where(a => a.BCM_BookId == model.BCM_BookId && a.BCM_ClassId == model.BCM_ClassId && a.BCM_IsDeleted == false).FirstOrDefault();
            if (existingRow != null)
            {
                return new Tuple<string, BooksClassMapping>("This book already mapped with same class.", null);
            }
            var entity = new BooksClassMapping();
            entity.BCM_CreationDate = System.DateTime.Now;
            entity.BCM_BookId = model.BCM_BookId;
            entity.BCM_ClassId = model.BCM_ClassId;
            entity.BCM_IsDeleted = false;
            entity.BCM_OutOfStock = false;
            entity.BCM_CreatedBy = model.UserId;
            entity.BCM_CreatedBy = model.UserId;
            entity.BCM_DefaultQty = model.BCM_DefaultQty;
            bMSContext.BooksClassMapping.Add(entity);
            bMSContext.SaveChanges();
            return new Tuple<string, BooksClassMapping>("", entity);
        }
        public PagedList<BooksClassMappingVM> GetBooksClassMapping(BooksClassMappingSearchVM searchModel)
        {
            var books = (from BCM in bMSContext.BooksClassMapping
                         join BM in bMSContext.BookMaster on BCM.BCM_BookId equals BM.Book_Id
                         join CM in bMSContext.ClassMaster on BCM.BCM_ClassId equals CM.Class_Id
                         orderby BCM.BCM_Id
                         where BCM.BCM_IsDeleted == false && BM.Book_BSMId == searchModel.UserId
                         select new BooksClassMappingVM()
                         {
                             BCM_Id = BCM.BCM_Id,
                             BCM_ClassId = BCM.BCM_ClassId,
                             BCM_BookId = BCM.BCM_BookId,
                             BCM_IsDeleted = BCM.BCM_IsDeleted,
                             BCM_OutOfStock = BCM.BCM_OutOfStock,
                             BookName = BM.Book_Name,
                             ClassName = CM.Class_Name,
                             BCM_DefaultQty = BCM.BCM_DefaultQty,
                         });
            return new PagedList<BooksClassMappingVM>(books, searchModel.PageIndex, searchModel.PageSize, new string[] { searchModel.SortDirection }, new string[] { searchModel.SortBy });
        }

        public bool DeleteBookClassMapping(int mappingId, int userId)
        {
            var row = bMSContext.BooksClassMapping.Where(a => a.BCM_Id == mappingId).FirstOrDefault();
            if (row != null)
            {
                row.BCM_IsDeleted = true;
                row.BCM_EditedBy = userId;
                row.BCM_EditedDate = System.DateTime.Now;
                bMSContext.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion

        public List<BookMasterVM> GetClassBooksForStudent(ClassBooksForStudentSearchModel searchModel)
        {
            var books = new List<BookMasterVM>();
            var mapping = (from SM in bMSContext.StudentMaster
                           join School in bMSContext.SchoolMaster on SM.St_SchoolId equals School.SM_Id
                           join BSSM in bMSContext.SellerSchoolMapping on School.SM_Id equals BSSM.SSM_SMId
                           where SM.St_id == searchModel.StudentId && BSSM.SSM_IsDeleted == false
                           select BSSM).FirstOrDefault();
            if (mapping != null)
            {
                books = (from BSM in bMSContext.BookMaster
                         join S in bMSContext.PublisherMaster on BSM.Book_PublisherId equals S.Publisher_id
                         join BTM in bMSContext.BookTypeMaster on BSM.Book_TypeId equals BTM.BT_Id
                         join BCM in bMSContext.BooksClassMapping on BSM.Book_Id equals BCM.BCM_BookId
                         orderby BSM.Book_Id
                         where BCM.BCM_ClassId == searchModel.ClassId && BCM.BCM_IsDeleted == false && BSM.Book_BSMId == mapping.SSM_BSMId
                         select new BookMasterVM()
                         {
                             Book_Id = BSM.Book_Id,
                             Book_Name = BSM.Book_Name,
                             Book_ShortName = BSM.Book_ShortName,
                             Book_PublisherId = BSM.Book_PublisherId,
                             Book_Price = BSM.Book_Price,
                             Book_CreationDate = BSM.Book_CreationDate,
                             Book_CreatedBy = BSM.Book_CreatedBy,
                             Book_EditedDate = BSM.Book_EditedDate,
                             Book_EditedBy = BSM.Book_EditedBy,
                             PublisherName = S.Publisher_Name,
                             BookType = BTM.BT_Name,
                             DefaultQuantity = BCM.BCM_DefaultQty,
                             Book_Image = BSM.Book_Image,
                             Book_BSMId = BSM.Book_BSMId
                         }).ToList();
                if (books != null)
                {
                    int rn = 1;
                    books.ForEach(m => { m.RN = rn++; });
                }
            }
            return books;
        }
    }
}
