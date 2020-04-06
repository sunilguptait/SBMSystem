
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

namespace SBMS.Mobile.ViewModels.Orders
{
    public class BuyBookViewModel : BaseViewModel
    {
        private readonly IOrderService _orderService;

        public ICommand LoadListDataCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand BookSelectCommand { get; set; }
        public ICommand QuantityIncreaseCommand { get; set; }
        public ICommand QuantityDecreaseCommand { get; set; }
        public ICommand BookSelectionChangeCommand { get; set; }
        public ICommand ProceedCommand { get; set; }
        private List<BookModel> tempList;
        private ObservableCollection<BookModel> _list;
        public ObservableCollection<BookModel> List
        {
            get { return _list; }
            set { SetProperty(ref _list, value); }
        }
        private BookModel _selectedItem;
        public BookModel SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
        private decimal _totalBooksAmount;
        public decimal TotalBooksAmount
        {
            get { return _totalBooksAmount; }
            set { SetProperty(ref _totalBooksAmount, value); }
        }
        private int _totalBooks;
        public int TotalBooks
        {
            get { return _totalBooks; }
            set { SetProperty(ref _totalBooks, value); }
        }
        public StudentModel StudentDetails { get; set; }
        public int PaymentMode { get; set; }
        int MaxBookQuantity = 50;
        public BuyBookViewModel(IOrderService orderService)
        {
            _orderService = orderService;
            LoadListDataCommand = new Command<int>(async vm => LoadListData(vm));
            BookSelectCommand = new Command(OnBookSelect);
            QuantityIncreaseCommand = new Command(OnQuantityIncrease);
            QuantityDecreaseCommand = new Command(OnQuantityDecrease);
            BookSelectionChangeCommand = new Command(OnBookSelectionChange);
            ProceedCommand = new Command<string>(OnProceedClick);

            RefreshCommand = new Command(async vm => OnRefreshClick());
            //LoadData();
        }
        public async Task LoadData()
        {
            await LoadListData();
        }
        public async Task OnRefreshClick()
        {
            FetchedRecords = -1;
            List = new ObservableCollection<BookModel>();
            await LoadListData();
        }

        public async Task LoadListData(int CallFrom = 0)
        {
            if (!IsFetchMore)
                return;

            if (List == null || CallFrom == 0)
                List = new ObservableCollection<BookModel>();

            ShowLoader = true;

            var response = await _orderService.GetClassBooksForStudent(new BookSearchModel() { ClassId = Convert.ToInt32(StudentDetails.ClassId), StudentId = StudentDetails.Id });
            if (!response.Success)
            {
                ShowLoader = false;
                DisplayError(response.ErrorMessage);
                await _pageService.PopAsync();
                return;
            }

            // Model = response.Data;
            NormanizeList(response);
            ShowLoader = false;
        }
        void NormanizeList(ApiBaseModel<ObservableCollection<BookModel>> response)
        {
            if (response?.Data == null)
                return;

            foreach (var item in response?.Data)
            {
                //if (item.DefaultQuantity == 1)
                //{
                //    item.IsDecrementButtonEnabled = false;
                //    item.IsIncrementButtonEnabled = false;
                //}
                item.Quantity = item.DefaultQuantity;
                item.IsDecrementButtonEnabled = item.Quantity != 1;
                item.IsIncrementButtonEnabled = item.Quantity != item.DefaultQuantity;
                List.Add(item);
            }
            FetchedRecords = Convert.ToInt32(response?.Data.Count);
            IsShowEmptyView = List.Count > 0 ? false : true;

            GetTotalSelectedBooks();
            GetTotalBooksAmount();
        }
        public async void OnBookSelect()
        {
            if (SelectedItem == null)
                return;

            if (IsBusy)
                return;
            IsBusy = true;
            //int orderId = Convert.ToInt32(SelectedItem?.Id);
            //await _pageService.PushAsync(new CommonWebView($"{App.GetPropertyValue("DMSApiBaseURL")}/orderdetails/29?isMobileClient=true", "Order Details"));
            SelectedItem = null;
            IsBusy = false;
        }
        public async void OnBookSelectionChange(object item)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var selectedItem = (BookModel)item;
            if (selectedItem == null)
                return;

            if (selectedItem.IsSelected)
            {
                selectedItem.IsSelected = false;
                var itemIndex = List.IndexOf(selectedItem);
                selectedItem.ItemOrignalIndex = itemIndex;
                List.RemoveAt(itemIndex);
                List.Add(CommonHelpers.Clone<BookModel>(selectedItem));
            }
            else
            {
                selectedItem.IsSelected = true;
                var itemIndex = List.IndexOf(selectedItem);
                List.RemoveAt(itemIndex);
                List.Insert(selectedItem.ItemOrignalIndex, CommonHelpers.Clone<BookModel>(selectedItem));
            }
            GetTotalSelectedBooks();
            GetTotalBooksAmount();
            IsBusy = false;
        }
        public async void OnQuantityIncrease(object item)
        {
            OnQuantityChange(item, 1);
        }
        public async void OnQuantityDecrease(object item)
        {
            OnQuantityChange(item, -1);
        }
        private async void OnQuantityChange(object item, int increasement)
        {
            if (IsBusy)
                return;

            var selectedItem = (BookModel)item;
            if (selectedItem == null)
                return;

            IsBusy = true;
            if ((increasement == 1 && selectedItem?.Quantity < selectedItem.DefaultQuantity) || (increasement == -1 && selectedItem?.Quantity > 1))
            {
                selectedItem.Quantity += increasement;
                selectedItem.IsDecrementButtonEnabled = selectedItem.Quantity != 1;
                selectedItem.IsIncrementButtonEnabled = selectedItem.Quantity != selectedItem.DefaultQuantity;
                var itemIndex = List.IndexOf(selectedItem);
                List.RemoveAt(itemIndex);
                List.Insert(itemIndex, CommonHelpers.Clone<BookModel>(selectedItem));
                GetTotalSelectedBooks();
                GetTotalBooksAmount();
            }
            IsBusy = false;
        }

        private void GetTotalSelectedBooks()
        {
            int totalBooks = List.Where(a => a.IsSelected == true).Sum(a => Convert.ToInt32(a.Quantity));
            TotalBooks = totalBooks;
        }
        private void GetTotalBooksAmount()
        {
            decimal totalBooksAmount = List.Where(a => a.IsSelected == true).Sum(a => Convert.ToInt32(a.Quantity) * a.Book_Price);
            TotalBooksAmount = totalBooksAmount;
        }
        /// <summary>
        /// proceedType=1 , Proceed from buy books page to summary page
        /// proceedType=2 , Proceed from summary page to payment options page
        /// </summary>
        /// <param name="proceedType"></param>
        public async void OnProceedClick(string proceedType = "1")
        {
            if (TotalBooks == 0)
            {
                DisplayError("Please atleast one book to proceed.");
                return;
            }

            if (IsBusy)
                return;

            IsBusy = true;
            if (proceedType == "1")
            {
                await _pageService.PushAsync(new BuyBooksSummary(this));
            }
            else if (proceedType == "2")
            {
                await _pageService.PushAsync(new PaymentOptions(this));
            }
            IsBusy = false;
        }
        public async void CreateOrder()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var orderModel = new OrderModel()
            {
                OrderDate = System.DateTime.Today,
                OrderPaymentMode = PaymentMode,
                OrderStatus = 1,
                TotalOrderAmount = TotalBooksAmount,
                StudentId = StudentDetails.Id,
                StudentEnrollmentId = StudentDetails.EnrollmentId,
                PaymentStatus=0,
                BookSellerId=List.FirstOrDefault()?.Book_BSMId,
                Books = List.Where(a => a.IsSelected == true).ToList()
            };
            
            var response = await _orderService.SaveOrder(orderModel);
            if (!response.Success)
            {
                ShowLoader = false;
                IsBusy = false;
                DisplayError(response.ErrorMessage);
                return;
            }
            MessagingCenter.Send<BuyBookViewModel>(this, "RefreshStudentsListAfterAddEdit");
            await _pageService.PushAsync(new OrderComplete(response.Data));
            ShowLoader = false;
            IsBusy = false;

        }
    }
}
