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
    public partial class PaymentOptions : ContentPage
    {
        public BuyBookViewModel _vm = null;
        public PaymentOptions(BuyBookViewModel vm)
        {
            _vm = vm;
            InitializeComponent();
            BindingContext = _vm;
        }

        private void Order_Clicked(object sender, EventArgs e)
        {
            if(!rbPayAtSchool.IsSelected)
            {
                _vm.DisplayError("Please select payment option to complete order");
                return;
            }
            _vm.PaymentMode = 1;
            _vm.CreateOrder();
        }
    }
}