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
    public partial class BuyBooksSummary : ContentPage
    {
        public BuyBookViewModel _vm = null;
        public BuyBooksSummary(BuyBookViewModel vm)
        {
            _vm = vm;
            InitializeComponent();
            BindItemSerialNo();
            BindingContext = _vm;
        }
        private void BindItemSerialNo()
        {
            int index = 1;
            foreach (var item in _vm.List?.Where(a => a.IsSelected == true))
            {
                item.ItemIndex = index;
                index++;
            }
        }


    }
}