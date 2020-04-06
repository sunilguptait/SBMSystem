using SBMS.Mobile.Common;
using SBMS.Mobile.ViewModels;
using SBMS.Mobile.Views.BookSeller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SBMS.Mobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        BaseViewModel _vm = new BaseViewModel();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = _vm;
            InitialzePage();
        }
        protected override async void OnAppearing()
        {
            try
            {
                if (App.GetPropertyValue("IsJustLoggedIn") == "1") // reInitalize page when user logged in
                {
                    App.SetPropertyValue("IsJustLoggedIn", "");
                    await InitialzePage();
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Message", ex.Message, "Ok");
            }

        }

        public async Task InitialzePage()
        {
            if (_vm.UserTypeId == (int)UserTypes.BookSeller)
            {
                var homePage = new BookSellerHomePage();
                homePage.IconImageSource = "Home";
                homePage.Title = "Home";
                this.Children[0] = homePage;
            }
            ////bool isAndroid = Device.RuntimePlatform == Device.Android;

            //this.Children.Clear();

            //this.Children.Add(new HomePage() { Title = "Home", Icon = "icon" });
            //this.Children.Add(new Brands() { Title = "Brand", Icon = "icon" });
            //this.Children.Add(new Cart() { Title = "Cart", Icon = "icon" });
            //if (UserHelper.IsLoggedIn)
            //{
            //    this.Children.Add(new Account() { Title = "Account", Icon = "icon" });
            //}

        }

        private void TabChanged(object sender, EventArgs e)
        {
            var tabIndex = this.Children.IndexOf(this.CurrentPage);
            HomePageTitleBar.IsVisible = false;
            TransactionsTitleBar.IsVisible = false;
            AccountTitleBar.IsVisible = false;
            _vm.HasNavigationBar = true;
            if (tabIndex == 0)
                HomePageTitleBar.IsVisible = true;
            //else if (tabIndex == 1)
            //    TransactionsTitleBar.IsVisible = true;

            //else if (tabIndex == 2)
            //{
            //    AccountTitleBar.IsVisible = true;
            //    _vm.HasNavigationBar = false;
            //}
           
            else if (tabIndex == 1)
            {
                AccountTitleBar.IsVisible = true;
                _vm.HasNavigationBar = false;
            }
        }
    }
}
