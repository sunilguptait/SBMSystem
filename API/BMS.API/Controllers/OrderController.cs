using BMS.Common;
using BMS.Services.Class;
using BMS.Services.Order;
using BMS.ViewModels;
using BMS.ViewModels.Class;
using BMS.ViewModels.Common;
using BMS.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BMS.API.Controllers
{
    public class OrderController : BaseApiController
    {
        IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public ResponseModel<OrderResponseVM> Create(OrderVM model)
        {
            var response = new ResponseModel<OrderResponseVM>();
            var orderDetails = _orderService.SaveOrder(model);
            if (string.IsNullOrEmpty(orderDetails.Item2))
            {
                response.Data = new OrderResponseVM()
                {
                    OrderCode = orderDetails.Item1.Order_Code,
                    OrderId = orderDetails.Item1.Order_Id
                };
            }
            else
            {
                response.ErrorMessage = orderDetails.Item2;
            }
            return response;
        }

        [HttpPost]
        public ResponseModel<OrderResponseVM> Update(OrderVM model)
        {
            var response = new ResponseModel<OrderResponseVM>();
            var orderDetails = _orderService.UpdateOrder(model);
            if (string.IsNullOrEmpty(orderDetails.Item2))
            {
                response.Data = new OrderResponseVM()
                {
                    OrderCode = orderDetails.Item1.Order_Code,
                    OrderId = orderDetails.Item1.Order_Id
                };
            }
            else
            {
                response.ErrorMessage = orderDetails.Item2;
            }
            return response;
        }

        [HttpGet]
        public ResponseModel<OrderVM> GetOrder(int id = 0, string orderCode = "")
        {
            var response = new ResponseModel<OrderVM>();
            var orderDetails = _orderService.GetOrderWithDetailsByIdOrCode(id, orderCode);
            orderDetails.QRCode = orderDetails.QRCode.AddBaseURL();
            response.Data = orderDetails;
            return response;
        }

        [HttpPost]
        public ResponseModel<List<OrderListVM>> List(OrderSearchVM model)
        {
            var response = new ResponseModel<List<OrderListVM>>();
            var ordersList = _orderService.GetOrders(model);
            response.Data = ordersList.Item1;
            response.TotalItems = ordersList.Item2;
            return response;
        }

    }
}
