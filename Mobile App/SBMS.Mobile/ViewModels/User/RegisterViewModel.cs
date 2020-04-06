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
    public class RegisterViewModel : BaseViewModel
    {
        public ICommand SignUpCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        private readonly IUserService _userService;

        private RegisterModel model;
        public RegisterModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }
        List<string> genders = null;
        public List<string> Genders
        {
            get { return genders; }
            set { SetProperty(ref genders, value); }
        }


        public RegisterViewModel(IUserService userService)
        {
            _userService = userService;
            Genders = new List<string>() { "Male", "Female" };
            Model = new RegisterModel();
            SignUpCommand = new Command(OnSignUp);
            LoginCommand = new Command(OnLogin);

        }
        public async void OnSignUp()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await _pageService.ShowLoader();
            var response = await _userService.Register(Model);
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
                UserHelper.Login(response.Data);
                //_pageService.PopModalAsync();
            }
            else
            {
                DisplayError(ErrorMessageHepler.Common);
            }
            _pageService.HideLoader();
            IsBusy = false;

        }

        public void OnLogin()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            _pageService.PopAsync();
            //_pageService.PushAsync(new Login());
            IsBusy = false;
        }
    }
}
