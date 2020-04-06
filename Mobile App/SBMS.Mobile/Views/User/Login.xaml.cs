using SBMS.Mobile.ViewModels.User;
using Autofac;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI;
using static SBMS.Mobile.Common.ValidationExtensions;

namespace SBMS.Mobile.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        LoginViewModel _vm = null;
        public Login(Page page = null, int openerType = 0)
        {
            InitializeComponent();
            _vm = App.Container.Resolve<LoginViewModel>();
            _vm.NextPage = page;
            _vm.OpenerType = openerType;
            BindingContext = _vm;
        }
        private async void Login_Clicked(object sender, EventArgs e)
        {
            if (ValidatePage())
            {
                _vm.LoginCommand.Execute(null);
            }
        }
        private bool ValidatePage()
        {
            bool isValid = ValidateMaterialTextFields(new System.Collections.Generic.List<XF.Material.Forms.UI.MaterialTextField>() { UserName, Password });
            return isValid;
        }
        public void ChangeLoginType(object sender, EventArgs args)
        {
            _vm.Model.LoginType = _vm.Model.LoginType == 1 ? 2 : 1;
            if (_vm.Model.LoginType == 1)
            {
                btnChangeLoginType.Text = "Use Mobile No.";
                UserName.InputType = MaterialTextFieldInputType.Email;
                UserName.Placeholder = "Email Id";
                UserName.ErrorText = "Email Id is required.";
            }
            else
            {
                btnChangeLoginType.Text = "Use Email ID";
                UserName.InputType = MaterialTextFieldInputType.Telephone;
                UserName.Placeholder = "Mobile No";
                UserName.ErrorText = "Mobile no is required.";
            }
        }
        public void IsShowPassword(object sender, EventArgs args)
        {
            Password.InputType = Password.InputType == MaterialTextFieldInputType.Password ? MaterialTextFieldInputType.Default : MaterialTextFieldInputType.Password;
            PasswordImage.Source = Password.InputType == MaterialTextFieldInputType.Password ? "eye.png" : "eyehidden.png";
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }



}

