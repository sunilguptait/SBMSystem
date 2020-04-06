using SBMS.Mobile.Common;
using SBMS.Mobile.Helpers;
using SBMS.Mobile.Models.User;
using SBMS.Mobile.Services;
using SBMS.Mobile.Services.User;
using SBMS.Mobile.Views.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SBMS.Mobile.ViewModels.User
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }
        public ICommand SignUpCommand { get; set; }

        private readonly IUserService _userService;

        private LoginModel model;
        public LoginModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        public Page NextPage { get; set; }
        public int OpenerType = 0; // 0-PushAsync ,1-PushAsync ,-1 -Nothing
        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
            Model = new LoginModel();
            LoginCommand = new Command(OnClickLogin);
            ForgotPasswordCommand = new Command(OnFotgotPassword);
            SignUpCommand = new Command(OnSignUp);
        }
        public async void OnClickLogin()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await _pageService.ShowLoader();
            var response = await _userService.Login(model);
            if (!response.Success || string.IsNullOrEmpty(response?.Data?.AccessToken))
            {
                _pageService.HideLoader();
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                    DisplayError(response.ErrorMessage);
                else
                    DisplayError(MessageHelper.CommonErrorMessage);
                IsBusy = false;
                return;
            }
            if (response.Data!=null)
            {
                if (response.Data.UserType == (int)UserTypes.BookSeller || response.Data.UserType == (int)UserTypes.Parents)
                {
                    UserHelper.Login(response.Data);
                    //_pageService.PopModalAsync();

                    if (NextPage != null)
                    {
                        if (OpenerType == 0)
                            await _pageService.PushAsync(NextPage);
                        else if (OpenerType == 1)
                            await _pageService.PushAsync(NextPage);
                    }
                }
                else
                {
                    DisplayError("You are not valid user to login here. Please contact to system admin.");
                }
            }
            else
            {
                DisplayError("User not found.");
            }
            _pageService.HideLoader();
            IsBusy = false;

        }

        public async void OnFotgotPassword()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await _pageService.PushAsync(new ForgotPassword());
            IsBusy = false;
        }
        public void OnSignUp()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            _pageService.PushAsync(new ValidateParentsRegistration());
            IsBusy = false;
        }
    }
}
