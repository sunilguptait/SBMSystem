using SBMS.Mobile.Helpers;
using SBMS.Mobile.Models.User;
using SBMS.Mobile.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SBMS.Mobile.Views.User
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Account : ContentPage
	{
        public AccountViewModel _vm = null;
		public Account ()
		{
			InitializeComponent ();
		}
        protected override async void OnAppearing()
        {
            try
            {
                if (_vm == null)
                {
                    _vm = new AccountViewModel();
                    BindingContext = _vm;
                }
                _vm.Model = new AccountModel() { UserName = UserHelper.UserName, UserImage = UserHelper.UserImagePath };
            }
            catch (Exception ex)
            {

            }
        }
    }
}