using SBMS.Mobile.Services;
using SBMS.Mobile.ViewModels;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SBMS.Mobile.ViewModels.User;
using SBMS.Mobile.ViewModels.Student;
using SBMS.Mobile.ViewModels.Orders;

namespace SBMS.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        StudentViewModel _vm = null;
        public HomePage()
        {
            //_vm = new HomePageViewModel();
            _vm = App.Container.Resolve<StudentViewModel>();
            InitializeComponent();
            BindingContext = _vm;
            MessageCenter();
        }
        protected override async void OnAppearing()
        {
            try
            {
                if (_vm.List == null)
                {
                    //_vm = App.Container.Resolve<HomePageViewModel>();
                    //await _vm.LoadData();
                    _vm.LoadData();
                }
            }
            catch (Exception ex)
            {

            }
        }
        void MessageCenter()
        {
            MessagingCenter.Subscribe<AddEditStudentViewModel>(this, "RefreshStudentsListAfterAddEdit", (sender) =>
            {
                _vm.OnRefreshClick();
            });
            MessagingCenter.Subscribe<BuyBookViewModel>(this, "RefreshStudentsListAfterAddEdit", (sender) =>
            {
                _vm.OnRefreshClick();
            });
        }

       
    }
}