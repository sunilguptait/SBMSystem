using SBMS.Mobile.ViewModels.User;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI;
using static SBMS.Mobile.Common.ValidationExtensions;

namespace SBMS.Mobile.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParentsRegistration : ContentPage
    {
        ParentsRegistrationViewModel _vm = null;
        string MobileNo = string.Empty;
        public ParentsRegistration(string mobileNo)
        {
            MobileNo = mobileNo;
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            try
            {
                if (_vm == null)
                {
                    _vm = App.Container.Resolve<ParentsRegistrationViewModel>();
                    _vm.Model.MobileNo = MobileNo;
                    BindingContext = _vm;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Signup_Clicked(object sender, EventArgs e)
        {
            if (ValidatePage())
            {
                _vm.SignUpCommand.Execute(null);
            }
        }
        private bool ValidatePage()
        {
            bool isValid = ValidateMaterialTextFields(new System.Collections.Generic.List<XF.Material.Forms.UI.MaterialTextField>()
            {  Name,States,Cities, Password, ConfirmPassword });

            if (isValid)
            {
                if (string.IsNullOrEmpty(Email.Text) && !Email.Text.IsEmail())
                {
                    Email.HasError = true;
                    Email.ErrorText = "Invalid email address";
                    isValid = false;
                }
                else
                {
                    Email.HasError = false;
                }
                if (Password.Text != ConfirmPassword.Text)
                {
                    ConfirmPassword.ErrorText = "Confirm password not matched.";
                    ConfirmPassword.HasError = true;
                    isValid = false;
                }
            }
            return isValid;
        }

        private void States_TextChanged(object sender, TextChangedEventArgs e)
        {
            _vm.BindDropdownValuesInModel();
            _vm.GetCities();
        }
    }
}