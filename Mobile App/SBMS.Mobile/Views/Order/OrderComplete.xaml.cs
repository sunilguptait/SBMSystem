using Autofac;
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
    public partial class OrderComplete : ContentPage
    {
        public OrderResponseModel _model = null;
        public OrderComplete(OrderResponseModel model)
        {
            _model = model;
            InitializeComponent();
            BindingContext = _model;
        }

        private void Done_Clicked(object sender, EventArgs e)
        {
            PageService pageService = new PageService();
            pageService.PopToRootAsync();
        }
    }
}