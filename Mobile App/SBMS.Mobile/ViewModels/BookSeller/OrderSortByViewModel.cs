using Rg.Plugins.Popup.Services;
using SBMS.Mobile.Models.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SBMS.Mobile.ViewModels.BookSeller
{
    public class OrderSortByViewModel : BaseViewModel
    {
        public ICommand SortBySelectCommand { get; set; }

        private ObservableCollection<OrderSortByModel> sortByList;
        public ObservableCollection<OrderSortByModel> SortByList
        {
            get { return sortByList; }
            set { SetProperty(ref sortByList, value); }
        }

        public OrderSortByViewModel()
        {
            SortBySelectCommand = new Command(OnSortBySelectClick);
            LoadData();
        }
        private void LoadData()
        {
            SortByList = new ObservableCollection<OrderSortByModel>()
           {
               new OrderSortByModel(){ SortBy="Order_date",SortDirection="Asc",DisplayName="Order Date Ascending"},
               new OrderSortByModel(){ SortBy="Order_date",SortDirection="Desc",DisplayName="Order Date Descending"},

               new OrderSortByModel(){ SortBy="Order_Code",SortDirection="Asc",DisplayName="Order Number Ascending"},
               new OrderSortByModel(){ SortBy="Order_Code",SortDirection="Desc",DisplayName="Order Number Descending"},
           };
        }
        public async void OnSortBySelectClick(object selectedItem)
        {
            var item = (OrderSortByModel)selectedItem;
            if (item == null)
                return;
            MessagingCenter.Send<OrderSortByViewModel, OrderSortByModel>(this, "RefreshOrdersOnSortByClick", item);
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
