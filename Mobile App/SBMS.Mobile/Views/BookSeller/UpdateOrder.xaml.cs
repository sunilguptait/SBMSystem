using Rg.Plugins.Popup.Pages;
using SBMS.Mobile.Common;
using SBMS.Mobile.Models.Common;
using SBMS.Mobile.ViewModels.BookSeller;
using SBMS.Mobile.ViewModels.Orders;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SBMS.Mobile.Views.BookSeller
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateOrder : ContentPage
    {
        ViewOrderViewModel _vm = null;
        public UpdateOrder(ViewOrderViewModel vm)
        {
            _vm = vm;
            BindingContext = _vm;
            InitializeComponent();
            InitilizeLists();
        }
        public void InitilizeLists()
        {
            DeliveryStatus.Choices = EnumExtensions.EnumToList<OrderStatus>();
            PaymentStatus.Choices = EnumExtensions.EnumToList<PaymentStatus>();
        }
        private async void Submit_Clicked(object sender, EventArgs e)
        {
            _vm.Model.OrderStatus = (int)EnumExtensions.GetEnumValueFromDescription<OrderStatus>(DeliveryStatus.Text);
            _vm.Model.PaymentStatus = (int)EnumExtensions.GetEnumValueFromDescription<PaymentStatus>(PaymentStatus.Text);
             var signature=await signatureView.GetImageStreamAsync(SignatureImageFormat.Png);
            _vm.Model.ReceiverSignature = signature.ConvertToBase64String();
            _vm.UpdateOrder();
        }
    }
}