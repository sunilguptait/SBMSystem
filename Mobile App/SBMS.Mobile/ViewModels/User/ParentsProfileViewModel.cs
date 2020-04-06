using SBMS.Mobile.Helpers;
using SBMS.Mobile.Models.Common;
using SBMS.Mobile.Models.User;
using SBMS.Mobile.Services;
using SBMS.Mobile.Services.Common;
using SBMS.Mobile.Services.User;
using SBMS.Mobile.Views.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SBMS.Mobile.ViewModels.User
{
    public class ParentsProfileViewModel : BaseViewModel
    {
        public ICommand SaveProfileCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }

        private readonly IUserService _userService;
        private readonly ICommonService _commonService;

        private ParentsProfileModel model;
        public ParentsProfileModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }
        private ObservableCollection<StateModel> statesList;
        public ObservableCollection<StateModel> StatesList
        {
            get { return statesList; }
            set { SetProperty(ref statesList, value); }
        }
        private ObservableCollection<CityModel> citiesList;
        public ObservableCollection<CityModel> CitiesList
        {
            get { return citiesList; }
            set { SetProperty(ref citiesList, value); }
        }
        public ParentsProfileViewModel(IUserService userService, ICommonService commonService)
        {
            _userService = userService;
            _commonService = commonService;
            SaveProfileCommand = new Command(OnSaveProfile);
            ChangePasswordCommand = new Command(OnChangePassword);
            InitalizeLists();
            GetStates();
        }
        public void InitalizeLists()
        {
            StatesList = new ObservableCollection<StateModel>();
            CitiesList = new ObservableCollection<CityModel>();
            StatesList.Add(new StateModel() { StateId = 0, StateName = "Please wait..." });
            CitiesList.Add(new CityModel() { CityId = 0, CityName = "Please wait..." });
        }
        public async Task InitilizeModel()
        {
            Model = new ParentsProfileModel();
            //await _pageService.ShowLoader();
            var response = await _userService.GetParentsProfile(UserHelper.ParentsId);
            if (!response.Success)
            {
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                    DisplayError(response.ErrorMessage);
                IsBusy = false;
                return;
            }
            Model = response.Data;
            _pageService.HideLoader();
        }
        public async void OnSaveProfile()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            await _pageService.ShowLoader();
            BindDropdownValuesInModel();
            var response = await _userService.UpdateParentsProfile(Model);
            if (!response.Success)
            {
                _pageService.HideLoader();
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                    DisplayError(response.ErrorMessage);
                IsBusy = false;
                return;
            }
            DisplayMessage("Your profile updated successfully.");
            _pageService.HideLoader();
            App.SetPropertyValue("UserName", Model.Name);
            IsBusy = false;
            //MessagingCenter.Send<AddEditStudentViewModel>(this, "RefreshStudentsListAfterAddEdit");
            await _pageService.PopAsync();

        }
        public void BindDropdownValuesInModel()
        {
            var selectedState = StatesList.Where(a => a.StateName == Model.StateName).FirstOrDefault();
            if (selectedState != null)
            {
                Model.StateId = selectedState.StateId;
            }

            var selectedCity = CitiesList.Where(a => a.CityName == Model.CityName).FirstOrDefault();
            if (selectedCity != null)
            {
                Model.CityId = selectedCity.CityId;
            }
        }
        public async void GetStates()
        {
            var response = await _commonService.GetStates(0);
            if (!response.Success)
            {
                DisplayError(response.ErrorMessage);
                return;
            }
            StatesList = response.Data;
        }

        public async void GetCities()
        {
            await _pageService.ShowLoader();
            var response = await _commonService.GetCities(Model.StateId);
            if (!response.Success)
            {
                _pageService.HideLoader();
                DisplayError(response.ErrorMessage);
                return;
            }
            _pageService.HideLoader();
            CitiesList = response.Data;
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
