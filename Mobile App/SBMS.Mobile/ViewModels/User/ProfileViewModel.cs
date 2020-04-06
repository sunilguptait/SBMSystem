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
    public class ProfileViewModel : BaseViewModel
    {
        public ICommand SaveProfileCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }

        private readonly IUserService _userService;

        private ProfileModel model;
        public ProfileModel Model
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


        public ProfileViewModel(IUserService userService)
        {
            _userService = userService;
            Genders = new List<string>() { "Male", "Female" };
            SaveProfileCommand = new Command(OnSaveProfile);
            ChangePasswordCommand = new Command(OnChangePassword);
            InitilizeModel();

        }
        void InitilizeModel()
        {
            Model = new ProfileModel();
        }
        public async void OnSaveProfile()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await _pageService.ShowLoader();
            //var currentCustomer = UserHelper.CustomerModel;
            //currentCustomer.FirstName = Model.FirstName;
            //currentCustomer.LastName = Model.LastName;
            //var response = await _userService.UpdateCustomer(currentCustomer);
            //if (!response.Success)
            //{
            //    _pageService.HideLoader();
            //    if (!string.IsNullOrEmpty(response.ErrorMessage))
            //        DisplayError(response.ErrorMessage);
            //    IsBusy = false;
            //    return;
            //}
            DisplayMessage("Profile Updated Scuccessfully");
            _pageService.HideLoader();
            IsBusy = false;

        }

        public void OnChangePassword()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            //_pageService.PopAsync();
            _pageService.PushAsync(new ChangePassword());
            IsBusy = false;
        }
      
    }
}
