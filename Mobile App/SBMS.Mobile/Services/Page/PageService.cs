using SBMS.Mobile.Helpers;
using SBMS.Mobile.Views.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace SBMS.Mobile.Services
{
    public class PageService : IPageService
    {
        // INavigation _Navigation => Application.Current?.MainPage?.Navigation;
        INavigation _Navigation => App.NavigationPage?.Navigation;
        IMaterialModalPage materialDialog;
        public void DisplayAlert(string title, string message, string ok)
        {
            Application.Current.MainPage.DisplayAlert(title, message, ok);
        }
        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, ok, cancel);
        }
        public async Task<string> DisplayActionSheet(string title, string cancel, string destruction, string[] buttons)
        {
            return await Application.Current.MainPage.DisplayActionSheet(title, cancel, "", buttons);
        }
        public async Task ShowLoader(string message)
        {
            materialDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: message);
        }
        public void HideLoader()
        {
            materialDialog?.DismissAsync();
        }
        public async Task ShowError(string message)
        {
            await MaterialDialog.Instance.SnackbarAsync(message: message,
                                           actionButtonText: "Got It",
                                           msDuration: 3000);
        }
        public async Task ShowSuccess(string message)
        {
            await MaterialDialog.Instance.SnackbarAsync(message: message,
                                            actionButtonText: "Got It",
                                            msDuration: 3000);
        }
        public async Task PushAsync(Page page)
        {
            var task = _Navigation.PushAsync(page);
            if (task != null)
                await task;
        }
        public async Task PopAsync()
        {
            if (_Navigation.NavigationStack.Count > 0)
            {
                var task = _Navigation.PopAsync();
                if (task != null)
                    await task;
            }
        }
        public async Task PopAllAsync()
        {
            //while (_Navigation.NavigationStack.Count() > 0)
            //{
            //    var task = _Navigation.PopAsync();
            //    if (task != null)
            //        await task;
            //}
        }
        public async Task PushModelAsync(Page page)
        {
            var task = _Navigation.PushModalAsync(page);
            if (task != null)
                await task;
        }
        public async Task PopModalAsync()
        {
            if (_Navigation.ModalStack.Count > 0)
            {
                var task = _Navigation.PopModalAsync();
                if (task != null)
                    await task;
            }
        }
        public async Task PopAllModalAsync()
        {
            while (_Navigation.ModalStack.Count() > 0)
            {
                var task = _Navigation.PopModalAsync();
                if (task != null)
                    await task;
            }
        }
        public async Task PopToRootAsync()
        {
            var task = _Navigation?.PopToRootAsync();
            if (task != null)
                await task;
        }

        public async Task CheckLogin(Page page = null)
        {
            if (UserHelper.IsLoggedIn)
            {
                await PushAsync(page);
            }
            else
            {
                //var result = await DisplayAlert("Alert", MessageHelper.NeedLogin, "Yes", "No");
                //if (result)
                //{
                //await PushAsync(new Login(page));
                //}

                //await PushAsync(new Login(page));
                await PushModelAsync(new Login(page));
            }
        }

        //public async Task<bool> CheckAppCurrentVersion()
        //{
        //    try
        //    {
        //        string appVersions = DependencyService.Get<Service.ShareService.IShare>().AppCurrentVersion;
        //        //ICommonService _commonService = new CommonService();
        //        //var data = await _commonService.GetAdminSetting<AdminSettingsModel>("Android_Version");
        //        if (Device.RuntimePlatform != Device.iOS)
        //        {
        //            string appCurrentVersion = PageHelper.GetValueFromCacheHistory(Device.RuntimePlatform == Device.iOS ? "IOS Version" : "Android Version");

        //            if (!string.IsNullOrEmpty(appCurrentVersion))
        //            {
        //                if (string.IsNullOrEmpty(appCurrentVersion))
        //                    return true;
        //                var splitter = appCurrentVersion.Split(',');
        //                if (splitter.Count() > 1 && splitter[1] == "1" && appVersions != splitter[0])
        //                {
        //                    await PushModelAsync(new AppForceUpdate());
        //                    return false;
        //                }
        //                else
        //                {
        //                    if (appVersions != splitter[0])
        //                    {
        //                        var responce = await DisplayAlert("Message", MessageHelper.NewAppVersionMessage, "Yes", "No");
        //                        if (responce)
        //                        {
        //                            if (Device.RuntimePlatform == Device.Android)
        //                                await Plugin.Share.CrossShare.Current.OpenBrowser("https://play.google.com/store/apps/details?id=com.FGDemo.android_v1");
        //                            //else

        //                        }
        //                        return false;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return true;
        //}

    }
}
