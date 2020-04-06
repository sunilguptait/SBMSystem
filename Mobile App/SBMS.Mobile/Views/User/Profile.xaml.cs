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
    public partial class Profile : ContentPage
    {
        ProfileViewModel _vm = null;
        public Profile()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            try
            {
                if (_vm == null)
                {
                    _vm = App.Container.Resolve<ProfileViewModel>();
                    BindingContext = _vm;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            if (ValidatePage())
            {
                _vm.SaveProfileCommand.Execute(null);
            }
        }
        private bool ValidatePage()
        {
            bool isValid = ValidateMaterialTextFields(new System.Collections.Generic.List<XF.Material.Forms.UI.MaterialTextField>()
            { FirstName, LastName });

            _vm.Model.Gender = Male.IsSelected ? 1 : 2;
            return isValid;
        }

        private void GenderSelection(object sender, XF.Material.Forms.UI.SelectedChangedEventArgs e)
        {
            Male.SelectedChanged -= GenderSelection;
            Female.SelectedChanged -= GenderSelection;
            var radioButton = (MaterialRadioButton)sender;
            if (radioButton?.Text == "Male")
            {
                if (!Male.IsSelected)
                    Male.IsSelected = true;
                Female.IsSelected = false;
            }
            else
            {
                Male.IsSelected = false;
                if (!Female.IsSelected)
                    Female.IsSelected = true;
            }
            Male.SelectedChanged += GenderSelection;
            Female.SelectedChanged += GenderSelection;
        }
    }
}