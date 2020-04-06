using Autofac;
using SBMS.Mobile.Common;
using SBMS.Mobile.Helpers;
using SBMS.Mobile.Models.Order;
using SBMS.Mobile.Models.Student;
using SBMS.Mobile.Services;
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
    public partial class ViewOrder : ContentPage
    {
        public ViewOrderViewModel _vm = null;
        int OrderId = 0;
        string OrderCode = "";
        public ViewOrder(int orderId=0,string orderCode="")
        {
            OrderId = orderId;
            OrderCode = orderCode;
            InitializeComponent();
            BindingContext = _vm;
            if (UserHelper.UserType == (int)UserTypes.BookSeller)
            {
                UpdateButton.Text = "Update";
                //this.ToolbarItems.Add(new ToolbarItem() { Text = "Update", Command = _vm.UpdateCommand });
            }
        }

        protected async override void OnAppearing()
        {
            
            if (_vm == null)
            {
                _vm = App.Container.Resolve<ViewOrderViewModel>();
                _vm.OrderCode = OrderCode;
                _vm.OrderId = OrderId;
                _vm.ShowLoader = true;
                BindingContext = _vm;
                _vm.LoadData();
                _vm.ShowLoader = false;
            }
            else
            {
                _vm.Model = CommonHelpers.Clone<OrderModel>(_vm.Model);
            }
        }

    }
}