using Autofac;
using SBMS.Mobile.Views.BookSeller;
using SBMS.Mobile.Helpers;
using SBMS.Mobile.Views.User;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SBMS.Mobile
{
    public partial class App : Application
    {
        public static NavigationPage NavigationPage { get; set; }
        public static IContainer Container;

        public App()
        {
            InitializeComponent();
            Bootstrapper.Initialize();
            XF.Material.Forms.Material.Init(this, "Material.Style");
            //IPageService pageService = new PageService();
            //pageService.ShowSuccess(Convert.ToString(UserHelper.CustomerId));
            if (!UserHelper.IsLoggedIn)
            {
                NavigationPage = new NavigationPage(new Login());
                MainPage = NavigationPage;
            }
            else
            {
                NavigationPage = new NavigationPage(new MainPage());
                MainPage = NavigationPage;
            }

        }

        protected override void OnStart()
        {
            // Handle when your app starts
            SetPropertyValue("SBMS.MobileApiBaseURL", "https://19d87004.ngrok.io");
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static dynamic GetPropertyValue(string propName)
        {
            //Preferences.Set("my_key", "my_value");
            if (Application.Current.Properties.ContainsKey(propName))
                return Application.Current.Properties[propName];
            return "";
        }
        public static void SetPropertyValue(string propName, dynamic value)
        {
            Application.Current.Properties[propName] = value;
            Application.Current.SavePropertiesAsync();
        }

    }
}
