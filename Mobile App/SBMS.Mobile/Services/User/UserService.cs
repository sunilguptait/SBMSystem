using SBMS.Mobile.Common;
using SBMS.Mobile.Models;
using SBMS.Mobile.Models.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using SBMS.Mobile.ViewModels.User;

namespace SBMS.Mobile.Services.User
{
    public class UserService : BaseService, IUserService
    {
        public async Task<ApiBaseModel<LoginResponseModel>> Login(LoginModel loginModel)
        {
            //var data = CommonExtensions.GetPropertyValues(loginModel);
            var responce = await _ApiClient.Post<LoginResponseModel>("api/Account/Login", loginModel);
            return responce;
        }
        public async Task<ApiBaseModel<LoginResponseModel>> Register(RegisterModel registerModel)
        {
            //var modelJson = CommonExtensions.GetPropertyValues(registerModel);
            var newModel = new { registration = registerModel };
            string modelJson = JsonConvert.SerializeObject(newModel);
            var responce = await _ApiClient.Post<LoginResponseModel>("api/registration", modelJson);
            return responce;
        }
       
        public async Task<ApiBaseModel<string>> ChangePassword(ChangePasswordModel model)
        {
            var responce = await _ApiClient.Post<string>("api/account/changepassword", model);
            return responce;
        }
        public async Task<ApiBaseModel<ChangePasswordResponseModel>> SendOTP(PasswordRecoveryModel model)
        {
            string modelJson = JsonConvert.SerializeObject(new { passwordrecovery = model });
            var responce = await _ApiClient.Post<ChangePasswordResponseModel>("api/PasswordRecoverySend", modelJson);
            return responce;
        }
        public async Task<ApiBaseModel<ChangePasswordResponseModel>> ValidateOTP(string mobileNo, string OTP)
        {
            var responce = await _ApiClient.Get<ChangePasswordResponseModel>($"api/CheckUserByMobileNoOtpApi?mobileNo={mobileNo}&otpNo={OTP}");
            return responce;
        }
        public async Task<ApiBaseModel<string>> ChangePasswordWiaForgotPassword(ChangePasswordModel model)
        {
            var responce = await _ApiClient.Post<string>("api/account/changepasswordwiaforgotpassword", model);
            return responce;
        }
        public async Task<ApiBaseModel<ValidateParentsUserNameResponseModel>> VerifyMobileNoOnForgotPassword(ValidateParentsUserNameModel model)
        {
            var responce = await _ApiClient.Post<ValidateParentsUserNameResponseModel>("api/account/verifymobilenoonforgotpassword", model);
            return responce;
        }

        public async Task<ApiBaseModel<OTPVerificationResponseModel>> VerifyOTP(OTPVerificationModel model)
        {
            var responce = await _ApiClient.Post<OTPVerificationResponseModel>("api/account/verifyotp", model);
            return responce;
        }
        public async Task<ApiBaseModel<ValidateParentsUserNameResponseModel>> ValidateUserOnParentsRegistration(ValidateParentsUserNameModel model)
        {
            var responce = await _ApiClient.Post<ValidateParentsUserNameResponseModel>("api/account/validateuseronparentsregistration", model);
            return responce;
        }
        public async Task<ApiBaseModel<LoginResponseModel>> ParentsRegistration(ParentsRegistrationModel model)
        {
            var responce = await _ApiClient.Post<LoginResponseModel>("api/account/parentsregistration", model);
            return responce;
        }
        public async Task<ApiBaseModel<ParentsProfileModel>> GetParentsProfile(int parentsId)
        {
            var responce = await _ApiClient.Get<ParentsProfileModel>("api/account/getparentsprofile?parentsId="+ parentsId);
            return responce;
        }
        public async Task<ApiBaseModel<string>> UpdateParentsProfile(ParentsProfileModel model)
        {
            var responce = await _ApiClient.Post<string>("api/account/updateparentsprofile", model);
            return responce;
        }

    }
}
