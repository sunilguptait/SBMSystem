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
using SBMS.Mobile.ViewModels.BookSeller;
using SBMS.Mobile.Models.Order;

namespace SBMS.Mobile.Views.BookSeller
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Orders : ContentPage
    {
        OrdersViewModel _vm = null;
        public Orders()
        {
            //_vm = new HomePageViewModel();
            _vm = App.Container.Resolve<OrdersViewModel>();
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
            //MessagingCenter.Subscribe<OrderSortByViewModel, OrderSortByModel>(this, "RefreshOrdersOnSortByClick", (sender, item) =>
            //{
            //    _vm.FilterModel.SortBy = item.SortBy;
            //    _vm.FilterModel.SortDirection = item.SortDirection;
            //    _vm.FetchedRecords = -1;
            //    _vm.LoadListData();
            //});
            MessagingCenter.Subscribe<ViewOrderViewModel>(this, "RefreshOrdersAfterUpdate", (sender) =>
            {
                _vm.OnRefreshClick();
            });
           

        }

    }
}