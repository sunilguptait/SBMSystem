using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Common;
using BMS.Data;
using BMS.Data.Entities;
using BMS.Services.EmailSender;
using BMS.Services.Users;
using BMS.ViewModels.BookSeller;
using BMS.ViewModels.Class;
using BMS.ViewModels.Common;
using BMS.ViewModels.User;

namespace BMS.Services.BookSeller
{
    public class BookSellerService : IBookSellerService
    {
        BMSContext bMSContext = new BMSContext();
        IUserService _userService;
        IEmailSenderService _emailSenderService;
        public BookSellerService(IUserService userService, IEmailSenderService emailSenderService)
        {
            _userService = userService;
            _emailSenderService = emailSenderService;
        }
        public BookSellerMaster GetBookSellerById(int id)
        {
            return bMSContext.BookSellerMaster.Where(m => m.BSM_Id == id).FirstOrDefault();
        }
        public BookSellerMaster SaveBookSeller(BookSellerVM model)
        {
            using (DbContextTransaction transaction = bMSContext.Database.BeginTransaction())
            {
                try
                {
                    var entity = GetBookSellerById(Convert.ToInt32(model.Id));
                    if (entity != null)
                    {
                        entity.BSM_FirstName = model.FirstName;
                        entity.BSM_LastName = model.LastName;
                        entity.BSM_FirmName = model.FirmName;
                        entity.BSM_RegistrationNo = model.RegistrationNo;
                        entity.BSM_Address1 = model.Address1;
                        entity.BSM_Address2 = model.Address2;
                        entity.BSM_CityId = model.CityId;
                        entity.BSM_StateId = model.StateId;
                        entity.BSM_PostCode = model.PostCode;
                        entity.BSM_MobileNo = model.MobileNo;
                        entity.BSM_EmailId = model.EmailId;
                        entity.BSM_Active = model.Active;
                        entity.BSM_EditedDate = DateTime.Now;
                        entity.BSM_EditedBy = model.EditedBy;
                        bMSContext.SaveChanges();
                    }
                    else
                    {
                        entity = new BookSellerMaster();
                        entity.BSM_FirstName = model.FirstName;
                        entity.BSM_LastName = model.LastName;
                        entity.BSM_FirmName = model.FirmName;
                        entity.BSM_RegistrationNo = model.RegistrationNo;
                        entity.BSM_Address1 = model.Address1;
                        entity.BSM_Address2 = model.Address2;
                        entity.BSM_CityId = model.CityId;
                        entity.BSM_StateId = model.StateId;
                        entity.BSM_PostCode = model.PostCode;
                        entity.BSM_MobileNo = model.MobileNo;
                        entity.BSM_EmailId = model.EmailId;
                        entity.BSM_Active = true;
                        entity.BSM_CreationDate = DateTime.Now;
                        entity.BSM_CreatedBy = model.CreatedBy;
                        bMSContext.BookSellerMaster.Add(entity);
                        bMSContext.SaveChanges();

                        //CREATE USER
                        var userModel = new CreateUserVM();
                        userModel.UserId = entity.BSM_Id;
                        userModel.UserType = (int)UserTypes.BookSeller;
                        userModel.UserName = entity.BSM_EmailId;
                        var userDetails = _userService.CreateUser(userModel);
                        SendRegistraionEmail(userDetails, model);
                    }
                    transaction.Commit();
                    return entity;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }
        public async void SendRegistraionEmail(CreateUserResponseVM userResponseVM, BookSellerVM bookSeller)
        {
            if (userResponseVM != null)
            {
                string subject = AppConstants.BookSellerRegistrationEmailSubject;
                string body = GetBookSellerRegistrationEmailContent(userResponseVM, bookSeller);
                _emailSenderService.SendEmail(subject, body, bookSeller.EmailId, bookSeller.FirstName, null, null, null);

            }
        }
        public string GetBookSellerRegistrationEmailContent(CreateUserResponseVM userResponseVM,BookSellerVM bookSeller)
        {
            string templatePath = "~/Views/Shared/EmailTemplates/BookSellerRegistrationEmailTemplate.html";
            StreamReader sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath(templatePath));
            StringBuilder strTemplate = new StringBuilder();
            strTemplate.Append(sr.ReadToEnd());
            sr.Close();
            strTemplate.Replace("#UserName#", bookSeller.FullName);
            strTemplate.Replace("#Password#", userResponseVM.Password);
            return strTemplate.ToString();
        }
        public PagedList<BookSellerVM> GetBookSellers(BookSellerSearchVM searchModel)
        {
            var bookSellers = (from BSM in bMSContext.BookSellerMaster
                               join S in bMSContext.StateMaster on BSM.BSM_StateId equals S.StateId
                               join C in bMSContext.CityMaster on BSM.BSM_CityId equals C.CityId
                               orderby BSM.BSM_Id
                               select new BookSellerVM()
                               {
                                   Id = BSM.BSM_Id,
                                   FirstName = BSM.BSM_FirstName,
                                   LastName = BSM.BSM_LastName,
                                   FirmName = BSM.BSM_FirmName,
                                   RegistrationNo = BSM.BSM_RegistrationNo,
                                   Address1 = BSM.BSM_Address1,
                                   Address2 = BSM.BSM_Address2,
                                   CityId = BSM.BSM_CityId,
                                   StateId = BSM.BSM_StateId,
                                   PostCode = BSM.BSM_PostCode,
                                   MobileNo = BSM.BSM_MobileNo,
                                   EmailId = BSM.BSM_EmailId,
                                   Active = BSM.BSM_Active,
                                   CreationDate = BSM.BSM_CreationDate,
                                   CreatedBy = BSM.BSM_CreatedBy,
                                   EditedBy = BSM.BSM_EditedBy,
                                   EditedDate = BSM.BSM_EditedDate,
                                   StateName = S.StateName,
                                   CityName = C.CityName,
                               });
            return new PagedList<BookSellerVM>(bookSellers, searchModel.PageIndex, searchModel.PageSize, new string[] { searchModel.SortDirection }, new string[] { searchModel.SortBy });
        }
        public List<DropdownVM> GetBookSellerDropdown()
        {
            var bookSellers = bMSContext.BookSellerMaster.Where(a => a.BSM_Active == true)
                .Select(a => new DropdownVM()
                {
                    Text = (a.BSM_FirstName + " " + a.BSM_LastName),
                    Value = a.BSM_Id
                })
                .ToList();
            return bookSellers;
        }

        #region Book Seller School Mapping
        public Tuple<string, SellerSchoolMapping> SaveSellerSchoolMapping(SellerSchoolMappingVM model)
        {
            var existingRow = bMSContext.SellerSchoolMapping.Where(a => a.SSM_SMId == model.SSM_SMId && a.SSM_IsDeleted == false).FirstOrDefault();
            if (existingRow != null)
            {
                return new Tuple<string, SellerSchoolMapping>("Selected school is already mapped with other school.", null);
            }
            var entity = new SellerSchoolMapping();
            entity.SSM_CreationDate = System.DateTime.Now;
            entity.SSM_BSMId = model.SSM_BSMId;
            entity.SSM_SMId = model.SSM_SMId;
            entity.SSM_IsDeleted = false;
            entity.SSM_CreatedBy = model.UserId;
            bMSContext.SellerSchoolMapping.Add(entity);
            bMSContext.SaveChanges();
            return new Tuple<string, SellerSchoolMapping>("", entity);
        }
        public PagedList<SellerSchoolMappingVM> GetSellerSchoolMapping(SellerSchoolMappingSearchVM searchModel)
        {
            var mappingList = (from SSM in bMSContext.SellerSchoolMapping
                               join BSM in bMSContext.BookSellerMaster on SSM.SSM_BSMId equals BSM.BSM_Id
                               join SM in bMSContext.SchoolMaster on SSM.SSM_SMId equals SM.SM_Id
                               orderby SSM.SSM_Id
                               where SSM.SSM_IsDeleted == false
                               select new SellerSchoolMappingVM()
                               {
                                   SSM_Id = SSM.SSM_Id,
                                   SSM_BSMId = SSM.SSM_BSMId,
                                   SSM_SMId = SSM.SSM_SMId,
                                   SSM_IsDeleted = SSM.SSM_IsDeleted,
                                   BookSellerName = (BSM.BSM_FirstName + " " + BSM.BSM_LastName),
                                   SchoolName = SM.SM_Name,
                               });
            return new PagedList<SellerSchoolMappingVM>(mappingList, searchModel.PageIndex, searchModel.PageSize, new string[] { searchModel.SortDirection }, new string[] { searchModel.SortBy });
        }
        public bool DeleteSellerSchoolMapping(int mappingId, int userId)
        {
            var row = bMSContext.SellerSchoolMapping.Where(a => a.SSM_Id == mappingId).FirstOrDefault();
            if (row != null)
            {
                row.SSM_IsDeleted = true;
                row.SSM_EditedBy = userId;
                row.SSM_EditedDate = System.DateTime.Now;
                bMSContext.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion

    }
}
