
using SBMS.Mobile.Models;
using SBMS.Mobile.Models.Order;
using SBMS.Mobile.Models.Student;
using SBMS.Mobile.Services.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using SBMS.Mobile.Helpers;
using SBMS.Mobile.Views.Order;
using SBMS.Mobile.Views.BookSeller;
using SBMS.Mobile.Common;

namespace SBMS.Mobile.ViewModels.Orders
{
    public class ViewOrderViewModel : BaseViewModel
    {
        private readonly IOrderService _orderService;

        public ICommand ProceedCommand { get; set; }
        public ICommand ViewQRCodeCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        private OrderModel _model;
        public OrderModel Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }

        public int OrderId { get; set; }
        public string OrderCode { get; set; }
        public ViewOrderViewModel(IOrderService orderService)
        {
            _orderService = orderService;
            //ProceedCommand = new Command<string>(OnProceedClick);
            ViewQRCodeCommand = new Command(OnViewQRCodeClick);
            UpdateCommand = new Command(OnUpdateClick);
        }
        public async Task LoadData()
        {
            await LoadListData();
        }
        public async Task OnRefreshClick()
        {
            FetchedRecords = -1;
            Model = new OrderModel();
            await LoadListData();
        }

        public async Task LoadListData(int CallFrom = 0)
        {
            if (!IsFetchMore)
                return;

            ShowLoader = true;

            var response = await _orderService.GetOrder(OrderId, OrderCode);
            if (!response.Success || response.Data == null)
            {
                ShowLoader = false;
                DisplayError(response.ErrorMessage ?? "Invalid order details. Please try again.");
                await _pageService.PopAsync();
                return;
            }
            Model = response.Data;
            IsShowEmptyView = true;
            BindItemSerialNo();
            ShowLoader = false;
        }
        private void BindItemSerialNo()
        {
            int index = 1;
            foreach (var item in Model.Books?.Where(a => a.IsSelected == true))
            {
                item.ItemIndex = index;
                index++;
            }
        }
        public async void OnViewQRCodeClick()
        {

            if (IsBusy)
                return;

            IsBusy = true;
            await _pageService.PushAsync(new ViewQRCode(Model.QRCode));
            IsBusy = false;
        }
        public async void OnUpdateClick()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            if (UserHelper.UserType == (int)UserTypes.BookSeller)
            {
                await _pageService.PushAsync(new UpdateOrder(this));
            }
            IsBusy = false;
        }

        public async void UpdateOrder()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var response = await _orderService.UpdateOrder(Model);
            if (!response.Success)
            {
                ShowLoader = false;
                IsBusy = false;
                DisplayError(response.ErrorMessage);
                return;
            }
            MessagingCenter.Send<ViewOrderViewModel>(this, "RefreshOrdersAfterUpdate");
            DisplayMessage("Order updated successfully.");
            _pageService.PopAsync();
            //await _pageService.PushAsync(new OrderComplete(response.Data));
            ShowLoader = false;
            IsBusy = false;

        }

    }
}
