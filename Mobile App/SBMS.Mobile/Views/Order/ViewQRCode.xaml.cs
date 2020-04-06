using Autofac;
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
    public partial class ViewQRCode : ContentPage
    {
        public string QRCodeImagePath { get; set; }
        public ViewQRCode(string qrCodeImagePath="")
        {
            QRCodeImagePath = qrCodeImagePath;
            InitializeComponent();
            BindingContext = this;
        }
    }
}