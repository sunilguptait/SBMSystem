
using SBMS.Mobile.Models;
using SBMS.Mobile.Models.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SBMS.Mobile.Services.Order
{
    public interface IOrderService
    {
        Task<ApiBaseModel<ObservableCollection<BookModel>>> GetClassBooksForStudent(BookSearchModel searchModel);
        Task<ApiBaseModel<OrderResponseModel>> SaveOrder(OrderModel model);
        Task<ApiBaseModel<OrderResponseModel>> UpdateOrder(OrderModel model);
        Task<ApiBaseModel<OrderModel>> GetOrder(int id, string orderCode);
        Task<ApiBaseModel<ObservableCollection<OrderListModel>>> GetOrders(OrderSearchModel searchModel);
    }
}
