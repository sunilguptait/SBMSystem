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
using System.Windows.Input;
using Xamarin.Forms;

namespace SBMS.Mobile.ViewModels.User
{
    public class ParentsRegistrationViewModel : BaseViewModel
    {
        public ICommand SignUpCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        private readonly IUserService _userService;
        private readonly ICommonService _commonService;

        private ParentsRegistrationModel model;
        public ParentsRegistrationModel Model
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

        public ParentsRegistrationViewModel(IUserService userService, ICommonService commonService)
        {
            _userService = userService;
            Genders = new List<string>() { "Male", "Female" };
            Model = new ParentsRegistrationModel();
            SignUpCommand = new Command(OnSignUp);
            LoginCommand = new Command(OnLogin);
            _commonService = commonService;
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
        public async void OnSignUp()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await _pageService.ShowLoader();
            var response = await _userService.ParentsRegistration(Model);
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
            if (response.Data != null)
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
