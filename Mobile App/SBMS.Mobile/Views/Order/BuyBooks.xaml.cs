using Autofac;
using SBMS.Mobile.Models.Student;
using SBMS.Mobile.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SBMS.Mobile.Views.Order
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuyBooks : ContentPage
    {
        public BuyBookViewModel _vm = null;
        StudentModel StudentDetails = null;
        public BuyBooks(StudentModel studentModel)
        {
            StudentDetails = studentModel;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (_vm == null)
            {
                _vm = App.Container.Resolve<BuyBookViewModel>();
                _vm.ShowLoader = true;
                _vm.StudentDetails = StudentDetails;
                BindingContext = _vm;
                _vm.LoadData();
                _vm.ShowLoader = false;
            }   
        }
    }
}