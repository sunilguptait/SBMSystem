using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Data.Entities;
using BMS.ViewModels.User;

namespace BMS.Services.Users
{
    public interface IUserService
    {
        List<User> GetUsers();
        UserLogin GetUserById(int id);
        UserLogin GetUserByUserName(string userName);
        CreateUserResponseVM CreateUser(CreateUserVM userVM);
        UserLoginResponseModel CheckLogin(string userName, string password);
        Tuple<bool, string> ChangePassword(ChangePasswordViewModel model);

        bool VerifyOTP(OTPVerificationVM model);
        #region Parents Registration
        ValidateParentsUserNameResponseVM ValidateUserOnParentsRegistration(ValidateParentsUserNameVM model);
        Tuple<ParentsMaster, string> ParentsRegistration(ParentsRegistrationVM model);
        ParentsProfileVM GetParentsProfile(int parentsId);
        void UpdateParentsProfile(ParentsProfileVM model);
        #endregion
    }
}
