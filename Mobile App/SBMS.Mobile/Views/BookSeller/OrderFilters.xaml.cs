using Rg.Plugins.Popup.Pages;
using SBMS.Mobile.Common;
using SBMS.Mobile.Models.Common;
using SBMS.Mobile.ViewModels.BookSeller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SBMS.Mobile.Views.BookSeller
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderFilters : ContentPage
    {
        OrdersViewModel _vm = null;
        public OrderFilters(OrdersViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            BindingContext = _vm;
            InitilizeLists();
        }
        public void InitilizeLists()
        {
            DeliveryStatus.Choices = EnumExtensions.EnumToList<OrderStatus>();
            PaymentStatus.Choices = EnumExtensions.EnumToList<PaymentStatus>();

            DeliveryStatus.Choices.Insert(0, "All");
            PaymentStatus.Choices.Insert(0, "All");
        }
        private void Apply_Clicked(object sender, EventArgs e)
        {
            _vm.FilterModel.DeliveryStatus = (int)EnumExtensions.GetEnumValueFromDescription<OrderStatus>(_vm.FilterModel.DeliveryStatusName);
            if (_vm.FilterModel.PaymentStatusName == "All" || string.IsNullOrEmpty(_vm.FilterModel.PaymentStatusName))
            {
                _vm.FilterModel.PaymentStatus = -1;
            }
            else
            {
                _vm.FilterModel.PaymentStatus = (int)EnumExtensions.GetEnumValueFromDescription<PaymentStatus>(_vm.FilterModel.PaymentStatusName);
            }
            _vm._pageService.PopAsync();
            _vm.OnRefreshClick();
        }
    }
}