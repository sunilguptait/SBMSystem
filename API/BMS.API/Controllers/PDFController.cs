using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BMS.Services.Order;
using BMS.ViewModels.Order;
using Rotativa;

namespace BMS.API.Controllers
{
    public class PDFController : BaseController
    {
        OrderService _orderService;
        public PDFController(OrderService orderService)
        {
            _orderService = orderService;
        }

        public ActionResult Invoice(InvoiceInputVM invoiceSearchVM)
        {
            //invoiceSearchVM = new InvoiceInputVM() { BookSellerId = 2, Orders = new List<string>() { "0000007" }, SessionId = 0 };
            var invoices = _orderService.GetInvoices(invoiceSearchVM);
            return new ViewAsPdf(invoices);
        }
    }
}