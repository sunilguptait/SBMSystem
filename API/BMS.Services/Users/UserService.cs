using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Common;
using BMS.Data;
using BMS.Data.Entities;
using BMS.ViewModels.User;

namespace BMS.Services.Users
{
    public class UserService : IUserService
    {
        BMSContext bMSContext = new BMSContext();

        public List<User> GetUsers()
        {
            return bMSContext.Users.ToList();
        }
        public UserLogin GetUserById(int id)
        {
            var user = bMSContext.UserLogin.Where(a => a.UL_Id == id).FirstOrDefault();
            return user;
        }
        public UserLogin GetUserByUserName(string userName)
        {
            var user = bMSContext.UserLogin.Where(a => a.UL_LoginName == userName).FirstOrDefault();
            return user;
        }
        public CreateUserResponseVM CreateUser(CreateUserVM userVM)
        {
            var response = new CreateUserResponseVM();
            var existUser = GetUserByUserName(userVM.UserName);
            if (existUser != null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Username already exists";
            }
            else
            {
                string password = CommonMethods.CreatePassword(8);
                string encryptedPass = CryptoEngine.Encrypt(userVM.Password ?? password);
                var entity = new UserLogin();
                entity.UL_LoginName = userVM.UserName;
                entity.UL_UserId = userVM.UserId;
                entity.UL_Type = userVM.UserType;
                entity.UL_Password = encryptedPass;
                entity.UL_CreatedBy = 1;
                entity.UL_CreationDate = System.DateTime.Now;
                entity.UL_Active = true;
                entity.UL_IsDefaultPassword = true;
                bMSContext.UserLogin.Add(entity);
                bMSContext.SaveChanges();

                response.IsSuccess = true;
                response.Password = password;
            }
            return response;
        }
        public UserLoginResponseModel CheckLogin(string userName, string password)
        {
            string encryptedPass = CryptoEngine.Encrypt(password);
            var data = (from user in bMSContext.UserLogin
                        where user.UL_LoginName.ToLower() == userName.ToLower() //&& user.UL_Password == encryptedPass && user.UL_Active == true
                        select new UserLoginResponseModel()
                        {
                            UserType = user.UL_Type,
                            UserId = user.UL_UserId,
                            IsDefaultPassword = user.UL_IsDefaultPassword

                        }).FirstOrDefault();
            if (data == null)
                return data;

            if (data.UserType == (int)UserTypes.Admin)
            {
                data.UserDisplayName = "admin";
            }
            else if (data.UserType == (int)UserTypes.BookSeller)
            {
                var bookSeller = bMSContext.BookSellerMaster.Where(a => a.BSM_Id == data.UserId).FirstOrDefault();
                if (bookSeller != null)
                {
                    data.UserDisplayName = bookSeller.BSM_FirstName + " " + bookSeller.BSM_LastName;
                    data.BookSellerId = bookSeller.BSM_Id;
                    data.Schools = (from u in bMSContext.SellerSchoolMapping
                                    join v in bMSContext.SchoolMaster on u.SSM_SMId equals v.SM_Id
                                    where u.SSM_BSMId == bookSeller.BSM_Id
                                    select new SchoolVM()
                                    {
                                        Id = v.SM_Id,
                                        Name = v.SM_Name
                                    }).ToList();
                }
            }
            else if (data.UserType == (int)UserTypes.Parents)
            {
                var parentRow = bMSContext.ParentsMaster.Where(a => a.P_Id == data.UserId).FirstOrDefault();
                if (parentRow != null)
                {
                    data.UserDisplayName = parentRow.P_Name;
                    data.ParentsId = parentRow.P_Id;
                }
            }

            return data;
        }

        public Tuple<bool, string> ChangePassword(ChangePasswordViewModel model)
        {
            var user = bMSContext.UserLogin.Where(a => a.UL_UserId == model.UserId && a.UL_Type == model.UserTypeId).FirstOrDefault();
            if (user == null)
            {
                return new Tuple<bool, string>(false, "Invalid User");
            }

            if (model.ValidateRequest)
            {
                string encryptedPass = CryptoEngine.Encrypt(model.OldPassword);
                if (encryptedPass != user.UL_Password)
                {
                    return new Tuple<bool, string>(false, "Old password does not match. Please try with valid old password.");
                }
            }
            string encryptedNewPass = CryptoEngine.Encrypt(model.NewPassword);
            user.UL_Password = encryptedNewPass;
            user.UL_IsDefaultPassword = false;
            bMSContext.SaveChanges();
            return new Tuple<bool, string>(true, "Password changed successfully");

        }
        public bool VerifyOTP(OTPVerificationVM model)
        {
            return true;
        }

        #region Parents Registration
        public ValidateParentsUserNameResponseVM ValidateUserOnParentsRegistration(ValidateParentsUserNameVM model)
        {
            var loginNameParam = new SqlParameter { ParameterName = "@LoginName", Value = (object)model.LoginName ?? DBNull.Value };
            var loginTypeParam = new SqlParameter { ParameterName = "@LoginType", Value = (object)model.LoginType ?? DBNull.Value };

            var details = bMSContext.Database.SqlQuery<ValidateParentsUserNameResponseVM>("EXEC ValidateUserOnParentsRegistration @LoginName,@LoginType",
                    loginNameParam, loginTypeParam).FirstOrDefault();
            return details;
        }

        public Tuple<ParentsMaster, string> ParentsRegistration(ParentsRegistrationVM model)
        {
            using (DbContextTransaction transaction = bMSContext.Database.BeginTransaction())
            {
                try
                {
                    //Save Parents Details
                    var entity = new ParentsMaster();
                    entity.P_Name = model.Name;
                    entity.P_MobileNo = model.MobileNo;
                    entity.P_EmailId = model.EmailId;
                    entity.P_CityId = model.CityId;
                    entity.P_StateId = model.StateId;
                    entity.P_CreatedBy = 0;
                    entity.P_CreationDate = System.DateTime.Now;
                    entity.P_Active = true;
                    bMSContext.ParentsMaster.Add(entity);

                    bMSContext.SaveChanges();

                    //CREATE USER
                    var userModel = new CreateUserVM();
                    userModel.UserId = entity.P_Id;
                    userModel.UserType = (int)UserTypes.Parents;
                    userModel.UserName = entity.P_MobileNo;
                    userModel.Password = model.Password;
                    var userDetails = CreateUser(userModel);
                    transaction.Commit();
                    return new Tuple<ParentsMaster, string>(entity, "");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Tuple<ParentsMaster, string>(null, "Error occurred while user registration. Please try again and contact to system admin.");
                }
            }
        }
        public ParentsProfileVM GetParentsProfile(int parentsId)
        {
            var parentsProfile = (from P in bMSContext.ParentsMaster
                                  join SM in bMSContext.StateMaster on P.P_StateId equals SM.StateId into PSM
                                  from SM in PSM.DefaultIfEmpty()
                                  join CM in bMSContext.CityMaster on P.P_CityId equals CM.CityId into PCM
                                  from CM in PCM.DefaultIfEmpty()
                                  where P.P_Id == parentsId
                                  select new ParentsProfileVM()
                                  {
                                      ParentsId = P.P_Id,
                                      Name = P.P_Name,
                                      Address1 = P.P_Address1,
                                      Address2 = P.P_Address2,
                                      CityId = P.P_CityId,
                                      CityName = CM.CityName,
                                      StateId = P.P_StateId,
                                      StateName = SM.StateName,
                                      PostCode = P.P_PostCode,
                                      MobileNo = P.P_MobileNo,
                                      EmailId = P.P_EmailId
                                  }).FirstOrDefault();
            return parentsProfile;
        }

        public void UpdateParentsProfile(ParentsProfileVM model)
        {
            var parentRow = bMSContext.ParentsMaster.Where(a => a.P_Id == model.ParentsId).FirstOrDefault();
            if (parentRow != null)
            {
                parentRow.P_Name = model.Name;
                parentRow.P_Address1 = model.Address1;
                parentRow.P_Address2 = model.Address2;
                parentRow.P_StateId = model.StateId;
                parentRow.P_CityId = model.CityId;
                parentRow.P_EditedBy = model.ParentsId;
                parentRow.P_PostCode = model.PostCode;
                parentRow.P_EditedDate = System.DateTime.Now;
                bMSContext.SaveChanges();
            }
        }

        #endregion
    }
}
