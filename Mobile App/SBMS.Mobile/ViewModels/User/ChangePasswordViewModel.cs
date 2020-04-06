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
    public class ChangePasswordViewModel : BaseViewModel
    {
        public ICommand ChangePasswordCommand { get; set; }

        private readonly IUserService _userService;

        private ChangePasswordModel model;
        public ChangePasswordModel Model
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


        public ChangePasswordViewModel(IUserService userService)
        {
            _userService = userService;
            Model = new ChangePasswordModel();
            ChangePasswordCommand = new Command(OnChangePassword);

        }
        public async void OnChangePassword()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await _pageService.ShowLoader();
            Model.UserId = UserHelper.UserId;
            var response = await _userService.ChangePassword(Model);
            if (!response.Success)
            {
                _pageService.HideLoader();
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                    DisplayError(response.ErrorMessage);
                else
                    DisplayError(MessageHelper.CommonErrorMessage);
                IsBusy = false;
                return;
            }
            Model = new ChangePasswordModel();
            DisplayMessage(response?.Data);
            _pageService.PopAsync();
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
