
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

using System.Linq;
using SBMS.Mobile.ViewModels;
using SBMS.Mobile.Services.Student;
using SBMS.Mobile.Models.Common;
using SBMS.Mobile.Common;
using SBMS.Mobile.Models;
using SBMS.Mobile.Models.Student;
using SBMS.Mobile.Views.User;
using XF.Material.Forms.Models;
using SBMS.Mobile.Views.Order;
using SBMS.Mobile.Services;
using ZXing.Net.Mobile.Forms;
using SBMS.Mobile.Services.Order;
using SBMS.Mobile.Models.Order;
using Rg.Plugins.Popup.Services;
using SBMS.Mobile.Views.Common;
using SBMS.Mobile.Views.BookSeller;

namespace SBMS.Mobile.ViewModels.BookSeller
{
    public class OrdersViewModel : BaseViewModel
    {
        private readonly IOrderService _orderService;

        public ICommand LoadListDataCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand OrderSelectCommand { get; set; }
        public ICommand CreateNewOrderCommand { get; set; }
        public ICommand ViewOrderCommand { get; set; }
        public ICommand SortByCommand { get; set; }
        public ICommand FilterCommand { get; set; }

        private ObservableCollection<OrderListModel> _list;
        public ObservableCollection<OrderListModel> List
        {
            get { return _list; }
            set { SetProperty(ref _list, value); }
        }
        private OrderListModel _selectedItem;
        public OrderListModel SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
        public OrderSearchModel FilterModel { get; set; }
        public OrdersViewModel(IOrderService orderService)
        {
            _orderService = orderService;
            LoadListDataCommand = new Command<int>(async vm => LoadListData(vm));
            CreateNewOrderCommand = new Command(OnCreateNewOrderClick);
            ViewOrderCommand = new Command<int>(OnViewOrderClick);
            RefreshCommand = new Command(async vm => OnRefreshClick());
            SortByCommand = new Command(OnSortByClick);
            FilterCommand = new Command(OnFilterClick);
            FilterModel = new OrderSearchModel();
            //LoadData();
        }
        public async Task LoadData()
        {
            await LoadListData();
        }
        public async Task OnRefreshClick()
        {
            FetchedRecords = -1;
            List = new ObservableCollection<OrderListModel>();
            await LoadListData();
        }

        public async Task LoadListData(int CallFrom = 0)
        {
            if (!IsFetchMore)
                return;

            if (List == null || CallFrom == 0)
                List = new ObservableCollection<OrderListModel>();

            ShowLoader = true;

            FilterModel.PageSize = PageSize;
            FilterModel.PageIndex = List.PageIndex(PageSize);

            var response = await _orderService.GetOrders(FilterModel);
            if (!response.Success)
            {
                ShowLoader = false;
                DisplayError(response.ErrorMessage);
                return;
            }

            // Model = response.Data;
            NormanizeList(response);
            ShowLoader = false;
        }
        void NormanizeList(ApiBaseModel<ObservableCollection<OrderListModel>> response)
        {
            if (response?.Data == null)
                return;

            foreach (var item in response?.Data)
            {
                List.Add(item);
            }
            FetchedRecords = Convert.ToInt32(response?.Data.Count);
            IsShowEmptyView = List.Count > 0 ? false : true;
        }

        public async void OnCreateNewOrderClick()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            //await _pageService.PushAsync(new AddEdit());
            IsBusy = false;
        }
        public async void OnViewOrderClick(int orderId)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await _pageService.PushAsync(new ViewOrder(orderId));
            IsBusy = false;
        }

        public async void OnSortByClick()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await PopupNavigation.Instance.PushAsync(new PopUp(new OrderSortBy()));
            IsBusy = false;
        }
        public async void OnFilterClick()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            //await PopupNavigation.Instance.PushAsync(new PopUp(new OrderFilters(this)));
            await _pageService.PushAsync(new OrderFilters(this));
            IsBusy = false;
        }
    }
}
