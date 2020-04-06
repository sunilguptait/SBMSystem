using BMS.API.JWT;
using BMS.Services.Users;
using BMS.ViewModels;
using BMS.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BMS.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [HttpGet]
        [AllowAnonymous]
        public ResponseModel<UserLoginResponseModel> Login(UserLoginVM model)
        {
            var response = new ResponseModel<UserLoginResponseModel>();
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                response.ErrorMessage = "Please provide username and password.";
                return response;
            }
            var userDetails = _userService.CheckLogin(model.UserName, model.Password);
            if (userDetails != null)
            {
                userDetails.LoginFrom = model.LoginFrom;
                JWTManager jWTManager = new JWTManager();
                userDetails.AccessToken = jWTManager.GenerateTokenForUser(userDetails, false);
                userDetails.RefreshToken = jWTManager.GenerateTokenForUser(userDetails, true);
                response.Data = userDetails;
            }
            else
            {
                response.ErrorMessage = "Invalid username and password.";
            }
            return response;
        }

        [HttpPost]
        public ResponseModel<UserLoginResponseModel> GetNewToken(UserLoginResponseModel model)
        {
            JWTManager jWTManager = new JWTManager();
            var response = new ResponseModel<UserLoginResponseModel>();
            var userModel = new UserLoginResponseModel();
            userModel.AccessToken = jWTManager.GenerateTokenForUser(model, false);
            userModel.RefreshToken = jWTManager.GenerateTokenForUser(model, true);
            response.Data = userModel;
            return response;
        }


        [HttpGet]
        public ResponseModel<bool> IsUserNameExists(string userName)
        {
            var response = new ResponseModel<bool>();
            var user = _userService.GetUserByUserName(userName);
            response.Data = user == null ? false : true;
            return response;
        }

        [HttpPost]
        [AllowAnonymous]
        public ResponseModel<OTPVerificationResponseModel> VerifyOTP(OTPVerificationVM model)
        {
            var response = new ResponseModel<OTPVerificationResponseModel>();
            if (string.IsNullOrEmpty(model.OTP) || string.IsNullOrEmpty(model.MobileNo))
            {
                response.ErrorMessage = "Provided details are not enough.";
                return response;
            }
            var resp = _userService.VerifyOTP(model);
            response.Data = new OTPVerificationResponseModel() { IsValid = resp };
            return response;
        }
        [HttpPost]
        [AllowAnonymous]
        public ResponseModel<ValidateParentsUserNameResponseVM> VerifyMobileNoOnForgotPassword(ValidateParentsUserNameVM model)
        {
            var response = new ResponseModel<ValidateParentsUserNameResponseVM>();
            var userDetails = _userService.ValidateUserOnParentsRegistration(model);
            if (userDetails != null)
            {
                if (userDetails.ReturnValue != 0)
                {
                    //SEND OTP CODE HERE...
                }
                else
                {
                    response.ErrorMessage = "You are not registered with entred mobile number. Please use registred mobile no.";
                }
                response.Data = userDetails;
            }
            else
            {
                response.ErrorMessage = "Something went wrong. Please try again.";
            }
            return response;
        }
        [HttpPost]
        [AllowAnonymous]
        public ResponseModel<string> ChangePasswordWiaForgotPassword(ChangePasswordViewModel model)
        {
            var response = new ResponseModel<string>();
            if (model.NewPassword != model.ConfirmPassword)
            {
                response.ErrorMessage = "Password and confim password does not matched.";
                return response;
            }
            model.ValidateRequest = false;
            var userDetails = _userService.ChangePassword(model);
            if (!userDetails.Item1)
                response.ErrorMessage = userDetails.Item2;
            else
                response.Data = "Your password has been reset successfully. Please try to login now.";
            return response;
        }

        [HttpPost]
        public ResponseModel<string> ChangePassword(ChangePasswordViewModel model)
        {
            var response = new ResponseModel<string>();
            if (model.NewPassword != model.ConfirmPassword)
            {
                response.ErrorMessage = "Password and confim password does not matched.";
                return response;
            }
            var userDetails = _userService.ChangePassword(model);
            if (!userDetails.Item1)
                response.ErrorMessage = userDetails.Item2;
            else
                response.Data = "Password changed successfully.";

            return response;
        }
        [HttpPost]
        [AllowAnonymous]
        public ResponseModel<ValidateParentsUserNameResponseVM> ValidateUserOnRegistration(ValidateParentsUserNameVM model)
        {
            var response = new ResponseModel<ValidateParentsUserNameResponseVM>();

            var userDetails = _userService.ValidateUserOnParentsRegistration(model);
            if (userDetails != null)
            {
                response.Data = userDetails;
            }
            else
            {
                response.ErrorMessage = "Something went wrong. Please try again.";
            }
            return response;
        }
        #region Parents Registration
        [HttpPost]
        [AllowAnonymous]
        public ResponseModel<ValidateParentsUserNameResponseVM> ValidateUserOnParentsRegistration(ValidateParentsUserNameVM model)
        {
            var response = new ResponseModel<ValidateParentsUserNameResponseVM>();

            var userDetails = _userService.ValidateUserOnParentsRegistration(model);
            if (userDetails != null)
            {
                if (userDetails.ReturnValue == 0)
                {
                    //SEND OTP CODE HERE...
                }
                response.Data = userDetails;
            }
            else
            {
                response.ErrorMessage = "Something went wrong. Please try again.";
            }
            return response;
        }

        [HttpPost]
        [AllowAnonymous]
        public ResponseModel<UserLoginResponseModel> ParentsRegistration(ParentsRegistrationVM model)
        {
            var response = new ResponseModel<UserLoginResponseModel>();

            var userDetails = _userService.ParentsRegistration(model);
            if (userDetails.Item1 != null && string.IsNullOrEmpty(userDetails.Item2))
            {
                return Login(new UserLoginVM() { Password = model.Password, UserName = model.MobileNo });
            }
            else
            {
                response.ErrorMessage = userDetails.Item2;
            }
            return response;
        }
        [HttpGet]
        public ResponseModel<ParentsProfileVM> GetParentsProfile(int parentsId)
        {
            var response = new ResponseModel<ParentsProfileVM>();
            var profileDetails = _userService.GetParentsProfile(parentsId);
            response.Data = profileDetails;
            return response;
        }

        [HttpPost]
        public ResponseModel<string> UpdateParentsProfile(ParentsProfileVM model)
        {
            var response = new ResponseModel<string>();
            _userService.UpdateParentsProfile(model);
            response.Data = "Profile updated successfully";
            return response;
        }

        #endregion
    }
}
