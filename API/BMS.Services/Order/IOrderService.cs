using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Common;
using BMS.Data.Entities;
using BMS.ViewModels.Order;

namespace BMS.Services.Order
{
    public interface IOrderService
    {
        OrderMaster GetClassById(int id);
        OrderVM GetOrderWithDetailsByIdOrCode(int id = 0, string orderCode = "");
        Tuple<OrderMaster, string> SaveOrder(OrderVM model);
        Tuple<OrderMaster, string> UpdateOrder(OrderVM model);
        Tuple<List<OrderListVM>, int> GetOrders(OrderSearchVM searchModel);
        List<InvoiceVM> GetInvoices(InvoiceInputVM searchModel);
    }
}
