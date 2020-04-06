using Autofac;
using SBMS.Mobile.Helpers;
using SBMS.Mobile.Models.User;
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
    public partial class ValidateParentsRegistration : ContentPage
    {
        BaseViewModel _vm = new BaseViewModel();
        IUserService _userService = null;
        public ValidateParentsRegistration()
        {
            InitializeComponent();
            _userService = App.Container.Resolve<IUserService>();
        }

        private async void Continue_Clicked(object sender, EventArgs e)
        {
            if (ValidateMaterialTextFields(new System.Collections.Generic.List<XF.Material.Forms.UI.MaterialTextField>() { MobileNo }))
            {
                if (!MobileNo.Text.IsPhoneNo())
                {
                    MobileNo.HasError = true;
                    MobileNo.ErrorText = "Invalid mobile no.";
                    return;
                }
                else
                {
                    MobileNo.HasError = false;
                }

                if (IsBusy)
                    return;
                IsBusy = true;

                await _vm._pageService.ShowLoader();
                var data = await _userService.ValidateUserOnParentsRegistration(new ValidateParentsUserNameModel() { LoginName = MobileNo.Text, LoginType = 1 });
                if (!data.Success)
                {
                    _vm.DisplayError(data.ErrorMessage);
                    goto EndTask;
                }
                if (data.Data.ReturnValue == 1)// ALREADY REGISTERD
                {
                    _vm.DisplayMessage(data.Data.ReturnMessage);
                    _vm._pageService.PopAsync();
                    _vm._pageService.PushAsync(new Login());
                    goto EndTask;
                }
                if (data.Data.ReturnValue == 1)// ALREADY REGISTERD
                {
                    _vm.DisplayMessage(data.Data.ReturnMessage);
                    _vm._pageService.PopAsync();
                    _vm._pageService.PushAsync(new ParentsRegistration(MobileNo.Text));
                    goto EndTask;
                }
                else if (data.Data.ReturnValue < 0)// REGISTERD WITH OTHER USER TYPE
                {
                    _vm.DisplayMessage(data.Data.ReturnMessage);
                    MobileNo.Text = "";
                    MobileNo.Focus();
                    goto EndTask;
                }
                else
                {
                    _vm.DisplayMessage(MessageHelper.OTPSentSuccessfully);
                    btnContinue.IsVisible = false;
                    StackOTP.IsVisible = true;
                    MobileNo.IsEnabled = false;
                }

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
                var data = await _userService.VerifyOTP(new OTPVerificationModel() { MobileNo = MobileNo.Text, OTP = OTP.Text });
                if (!data.Success)
                {
                    _vm.DisplayError(data.ErrorMessage);
                    goto EndTask;
                }
                _vm._pageService.PushAsync(new ParentsRegistration(MobileNo.Text));

                EndTask:
                _vm._pageService.HideLoader();
                IsBusy = false;
                return;
            }
        }

        private void MaterialButton_Clicked(object sender, EventArgs e)
        {
            _vm._pageService.PopAsync();
        }
    }
}