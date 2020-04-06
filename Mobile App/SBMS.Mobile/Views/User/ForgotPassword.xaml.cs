using Autofac;
using SBMS.Mobile.Helpers;
using SBMS.Mobile.Services.User;
using SBMS.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SBMS.Mobile.Common.ValidationExtensions;

namespace SBMS.Mobile.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPassword : ContentPage
    {
        BaseViewModel _vm = new BaseViewModel();
        IUserService _userService = null;
        int UserId = 0;
        public ForgotPassword()
        {
            InitializeComponent();
            _userService = App.Container.Resolve<IUserService>();
        }

        private async void Continue_Clicked(object sender, EventArgs e)
        {
            if (ValidateMaterialTextFields(new System.Collections.Generic.List<XF.Material.Forms.UI.MaterialTextField>() { MobileNo }))
            {
                if (IsBusy)
                    return;
                IsBusy = true;

                await _vm._pageService.ShowLoader();
                var data = await _userService.VerifyMobileNoOnForgotPassword(new Models.User.ValidateParentsUserNameModel() { LoginName = MobileNo.Text, LoginType = 1 });
                if (!data.Success)
                {
                    _vm.DisplayError(data.ErrorMessage);
                    goto EndTask;
                }
                UserId = data.Data.UserId;
                _vm.DisplayMessage(MessageHelper.OTPSentSuccessfully);
                btnContinue.IsVisible = false;
                StackOTP.IsVisible = true;
                MobileNo.IsEnabled = false;

                EndTask:
                _vm._pageService.HideLoader();
                IsBusy = false;
                return;
            }


            //Application.Current.MainPage = new Login();
        }

        private async void BtnSubmitOTP_Clicked(object sender, EventArgs e)
        {
            if (ValidateMaterialTextFields(new System.Collections.Generic.List<XF.Material.Forms.UI.MaterialTextField>() { MobileNo, OTP }))
            {
                if (IsBusy)
                    return;
                IsBusy = true;

                await _vm._pageService.ShowLoader();
                var data = await _userService.VerifyOTP(new Models.User.OTPVerificationModel() { MobileNo = MobileNo.Text, OTP = OTP.Text });
                if (!data.Success)
                {
                    _vm.DisplayError(data.ErrorMessage);
                    goto EndTask;
                }

                btnSubmitOTP.IsVisible = false;
                StackNewPassword.IsVisible = true;
                OTP.IsEnabled = false;

                EndTask:
                _vm._pageService.HideLoader();
                IsBusy = false;
                return;
            }
        }

        private async void ChangePassword_Clicked(object sender, EventArgs e)
        {
            if (ValidatePage())
            {
                if (IsBusy)
                    return;
                IsBusy = true;

                await _vm._pageService.ShowLoader();
                var data = await _userService.ChangePasswordWiaForgotPassword(new Models.User.ChangePasswordModel() { UserId=UserId,NewPassword = NewPassword.Text, ConfirmPassword = ConfirmPassword.Text });
                if (!data.Success)
                {
                    _vm.DisplayError(data.ErrorMessage);
                    goto EndTask;
                }
                _vm.DisplayError(data.Data);
                
                await _vm._pageService.PopAsync();

                EndTask:
                _vm._pageService.HideLoader();
                IsBusy = false;
                return;
            }
        }
        private bool ValidatePage()
        {
            var isValid = ValidateMaterialTextFields(new System.Collections.Generic.List<XF.Material.Forms.UI.MaterialTextField>() { MobileNo, OTP, NewPassword, ConfirmPassword });
            if (isValid)
            {
                if (NewPassword.Text != ConfirmPassword.Text)
                {
                    ConfirmPassword.ErrorText = "Confirm password not matched.";
                    ConfirmPassword.HasError = true;
                    isValid = false;
                }
            }
            return isValid;
        }
    }
}