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
    public partial class ParentsProfile : ContentPage
    {
        ParentsProfileViewModel _vm = null;
        public ParentsProfile()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            try
            {
                if (_vm == null)
                {
                    _vm = App.Container.Resolve<ParentsProfileViewModel>();
                    await _vm.InitilizeModel();
                    BindingContext = _vm;
                    States.Text = _vm.Model.StateName;
                    Cities.Text = _vm.Model.CityName;
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
            { FirstName,Address1,PostCode,Cities,States,Email  });

            return isValid;
        }
        private void States_TextChanged(object sender, TextChangedEventArgs e)
        {
            _vm.BindDropdownValuesInModel();
            _vm.Model.CityName = "";
            _vm.GetCities();
        }

    }
}