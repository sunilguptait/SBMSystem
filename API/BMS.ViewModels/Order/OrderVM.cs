using BMS.Common;
using BMS.ViewModels.Book;
using BMS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BMS.ViewModels.Order
{
    public class OrderVM : BaseViewModel
    {
        public OrderVM()
        {
            Books = new List<BookMasterVM>();
        }
        public int? OrderId { get; set; }
        public string OrderCode { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? StudentId { get; set; }
        public int? StudentEnrollmentId { get; set; }
        public int? OrderStatus { get; set; }
        public int? OrderPaymentMode { get; set; }
        public int? PaymentStatus { get; set; }
        public decimal? TotalOrderAmount { get; set; }
        public string QRCode { get; set; }
        public List<BookMasterVM> Books { get; set; }
        public string OrderPaymentRemark { get; set; }
        public string OrderStatusName { get { return Convert.ToString((OrderStatus)OrderStatus); } }
        public string PaymentModeName { get { return Convert.ToString((PaymentMode)OrderPaymentMode); } }
        public string PaymentStatusName { get { return Convert.ToString((PaymentStatus)PaymentStatus); } }
        public string ReceiverSignature { get; set; }
    }
    public class OrderResponseVM
    {
        public int OrderId { get; set; }
        public string OrderCode { get; set; }
    }

    public class OrderSearchVM : BaseSearchModel
    {
        public DateTime? OrderDate { get; set; }
        public int DeliveryStatus { get; set; } = 0;
        public int PaymentStatus { get; set; } = -1;
        public string StudentName { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
        public int SessionId { get; set; } = 0;
    }
    public class OrderListVM
    {
        public int Order_Id { get; set; }
        public string Order_Code { get; set; }
        public DateTime Order_date { get; set; }
        public int Order_SEId { get; set; }
        public int Order_Status { get; set; }
        public int Order_PaymentMode { get; set; }
        public int Order_PaymentStatus { get; set; }
        public decimal Order_TotalAmount { get; set; }
        public string St_Name { get; set; }
        public int St_id { get; set; }
        public string OrderStatusName { get { return Convert.ToString((OrderStatus)Order_Status); } }
        public string PaymentModeName { get { return Convert.ToString((PaymentMode)Order_PaymentMode); } }
        public string PaymentStatusName { get { return Convert.ToString((PaymentStatus)Order_PaymentStatus); } }

    }

}
