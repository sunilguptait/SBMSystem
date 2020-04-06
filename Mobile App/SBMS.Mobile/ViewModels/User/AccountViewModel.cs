using SBMS.Mobile.Helpers;
using SBMS.Mobile.Models.User;
using SBMS.Mobile.Views.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SBMS.Mobile.ViewModels.User
{
    public class AccountViewModel : BaseViewModel
    {
        public ICommand LogoutCommand { get; set; }
        public ICommand SelectMenuCommand { get; set; }
        public ICommand EditProfileCommand { get; set; }


        private List<MenuModel> _menuItems;
        public List<MenuModel> MenuItems
        {
            get { return _menuItems; }
            set { SetProperty(ref _menuItems, value); }
        }

        private AccountModel model;
        public AccountModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }
        private MenuModel _selectedItem;
        public MenuModel SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public AccountViewModel()
        {
            LogoutCommand = new Command(OnClickLogout);
            SelectMenuCommand = new Command(OnSelectMenu);
            EditProfileCommand = new Command(OnEditProfile);
            BindMenus();
        }
        void BindMenus()
        {
            MenuItems = new List<MenuModel>() {
                //new MenuModel(){ Title="My Orders", Icon="Order",PageName=new MyOrders()},
                ////new MenuModel(){ Title="Reward Point", Icon="Reward",PageName=new Register()},
                //new MenuModel(){ Title="Wishlist", Icon="wishlist",PageName=new Wishlist()},
                //new MenuModel(){ Title="My Address", Icon="Address",PageName=new MyAddress()},
                //new MenuModel(){ Title="Earning Reports", Icon="AboutUs",PageName=new Reports()},
                //new MenuModel(){ Title="Upload Documents", Icon="Download",PageName=new UploadDocuments()},
                ////new MenuModel(){ Title="Cash Wallet", Icon="RateUs",PageName=new ChangePassword()},
                new MenuModel(){ Title="Refer to others", Icon="Reward",PageName=new ChangePassword()},
                new MenuModel(){ Title="Change Password", Icon="change_password",PageName=new ChangePassword()},
                new MenuModel(){ Title="Logout", Icon="logout",PageName=new ChangePassword()},
            };

        }
        public async void OnClickLogout()
        {
            var result = await _pageService.DisplayAlert("Confirmation", "Are you sure you want to Logout?", "Yes", "No");
            if (result)
            {
                UserHelper.Logout();
            }
        }

        async void OnSelectMenu()
        {
            try
            {
                if (SelectedItem == null || IsBusy)
                    return;
                IsBusy = true;
                var pageName = SelectedItem?.PageName;
                if (SelectedItem?.Title.Trim().ToLower() == "logout")
                {
                    OnClickLogout();
                    //var result = await _pageService.DisplayAlert("Confirmation Notification", "Are you sure you want to Logout?", "Yes", "No");
                    //if (result)
                    //{
                    //    UserHelper.Logout();

                    //    INotifiationService notifiationService = new NotifiationService();
                    //    notifiationService.SaveDeviceAndNotificationInfo<string>(new DeviceInfoModel() { DeviceId = UserHelper.DeviceId, ActionType = "logout" });
                    //    //App.NavigationPage = new NavigationPage(new MainPage());
                    //    //Application.Current.MainPage = App.NavigationPage;
                    //    App.RootPage.Master = new MenuPage();
                    //    await _pageService.PopToRootAsync();
                    //}
                }
                else if (SelectedItem?.Title.Trim().ToLower() == "refer to others")
                {

                    ShareMessage("Refer & Earn", "Message Here....");
                    //ShareMessage message = new ShareMessage() { Title = "Share App", Text = ShareMessageHelper.AppShareText, Url = "https://play.google.com/store/apps/details?id=com.kayawell.android_v1" };
                    //ShareOptions options = new ShareOptions() { ChooserTitle = "Share App" };
                    //ISharePluginService _shareService = new SharePluginService();
                    //_shareService.ShareMessage(message, options);
                }
                else if (SelectedItem?.Title.Trim().ToLower() == "rate us")
                {
                    await Plugin.Share.CrossShare.Current.OpenBrowser("app path");
                }
                //else if (selectedMenu?.Title.Trim().ToLower() == "get support")
                //{
                //    var url = string.Format("{0}/home/getsupportapi?userName={1}&roleName={2}", App.GetPropertyValue("ApiBaseUrl"), App.GetPropertyValue("UserEmail"), UserHelper.IsUserLoggedIn == true ? "Member" : "Expert");
                //    await Plugin.Share.CrossShare.Current.OpenBrowser(url);
                //}

                else
                    await _pageService.PushAsync(pageName);

                SelectedItem = null;
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                //  ManageCatch(ex);
            }


            ////App.NavigationPage.Navigation.PushAsync(pageName);
            ////App.MenuIsPresented = false;

            //var menuPage = new MenuPage();
            //App.NavigationPage = new NavigationPage(pageName);
            ////  RootPage.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HomePage))) { BarBackgroundColor = Color.Red };
            //App.RootPage = new RootPage();
            //App.RootPage.Master = menuPage;
            //App.RootPage.Detail = App.NavigationPage;
            //Application.Current.MainPage = App.RootPage;

        }
        public void OnEditProfile()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            //_pageService.PopAsync();
            _pageService.PushAsync(new ParentsProfile());
            IsBusy = false;
        }
    }
}
