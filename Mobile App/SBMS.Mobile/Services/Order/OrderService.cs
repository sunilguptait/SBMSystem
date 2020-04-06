
using SBMS.Mobile.Models;
using SBMS.Mobile.Models.Order;
using SBMS.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SBMS.Mobile.Services.Order
{
    public class OrderService : BaseService, IOrderService
    {
        public async Task<ApiBaseModel<ObservableCollection<BookModel>>> GetClassBooksForStudent(BookSearchModel searchModel)
        {
            var responce = await _ApiClient.Post<ObservableCollection<BookModel>>("/api/book/getclassbooksforstudent", searchModel);
            return responce;
        }
        public async Task<ApiBaseModel<OrderResponseModel>> SaveOrder(OrderModel model)
        {
            var responce = await _ApiClient.Post<OrderResponseModel>("api/order/create", model);
            return responce;
        }
        public async Task<ApiBaseModel<OrderResponseModel>> UpdateOrder(OrderModel model)
        {
            var responce = await _ApiClient.Post<OrderResponseModel>("api/order/update", model);
            return responce;
        }
        public async Task<ApiBaseModel<OrderModel>> GetOrder(int id, string orderCode)
        {
            var responce = await _ApiClient.Get<OrderModel>($"api/order/getorder?id={id}&orderCode={orderCode}");
            return responce;
        }
        public async Task<ApiBaseModel<ObservableCollection<OrderListModel>>> GetOrders(OrderSearchModel searchModel)
        {
            var responce = await _ApiClient.Post<ObservableCollection<OrderListModel>>("api/order/list", searchModel);
            return responce;
        }
    }
}
