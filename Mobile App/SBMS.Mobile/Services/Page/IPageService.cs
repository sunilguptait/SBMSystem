using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SBMS.Mobile.Services
{
    public interface IPageService
    {
        void DisplayAlert(string title, string message, string ok);
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);
        Task<string> DisplayActionSheet(string title, string cancel, string destruction, string[] buttons);
        Task ShowLoader(string message = "Please Wait...");
        void HideLoader();
        Task ShowError(string message);
        Task ShowSuccess(string message);
        Task PushAsync(Page page);
        Task PopAsync();
        Task PopAllAsync();
        Task PushModelAsync(Page page);
        Task PopModalAsync();
        Task PopAllModalAsync();
        Task PopToRootAsync();
        Task CheckLogin(Page page);
        //Task<bool> CheckAppCurrentVersion();
    }
}
