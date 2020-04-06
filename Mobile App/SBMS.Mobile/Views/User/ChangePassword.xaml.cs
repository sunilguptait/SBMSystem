using SBMS.Mobile.ViewModels.User;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SBMS.Mobile.Common.ValidationExtensions;

namespace SBMS.Mobile.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePassword : ContentPage
    {
        ChangePasswordViewModel _vm = null;
        public ChangePassword()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            try
            {
                if (_vm == null)
                {
                    _vm = App.Container.Resolve<ChangePasswordViewModel>();
                    BindingContext = _vm;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void Submit_Clicked(object sender, EventArgs e)
        {
            if (ValidatePage())
            {
                _vm.ChangePasswordCommand.Execute(null);
            }
        }
        private bool ValidatePage()
        {
            bool isValid = ValidateMaterialTextFields(new System.Collections.Generic.List<XF.Material.Forms.UI.MaterialTextField>() { CurrentPassword, NewPassword, ConfirmPassword });
            if (isValid)
            {
                if (NewPassword.Text != ConfirmPassword.Text)
                {
                    ConfirmPassword.ErrorText = "Confirm password not matched.";
                    ConfirmPassword.HasError = true;
                    isValid = false;
                }
            }
            return isValid;
        }
    }
}