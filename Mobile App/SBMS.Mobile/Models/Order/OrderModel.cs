using Newtonsoft.Json;
using SBMS.Mobile.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XF.Material.Forms.Models;

namespace SBMS.Mobile.Models.Order
{
    [Serializable]
    public class OrderModel
    {
        public OrderModel()
        {
            Books = new List<BookModel>();
        }
        public int? OrderId { get; set; }
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public int? StudentId { get; set; }
        public int? StudentEnrollmentId { get; set; }
        public int? OrderStatus { get; set; }
        public int? OrderPaymentMode { get; set; }
        public int? PaymentStatus { get; set; }
        public decimal? TotalOrderAmount { get; set; }
        public string QRCode { get; set; }// = "sample_qr";
        public int? BookSellerId { get; set; }
        public List<BookModel> Books { get; set; }

        public string OrderStatusName { get { return Convert.ToString((OrderStatus)OrderStatus); } }
        public string PaymentStatusName { get { return Convert.ToString((PaymentStatus)PaymentStatus); } }
        public string ReceiverSignature { get; set; }
    }
    public class OrderResponseModel
    {
        public int OrderId { get; set; }
        public string OrderCode { get; set; }
    }
    public class OrderSearchModel : BaseParametersModel
    {
        public DateTime? OrderDate { get; set; }
        public int DeliveryStatus { get; set; } = 0;
        public int PaymentStatus { get; set; } = -1;
        public string StudentName { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
        public int SessionId { get; set; } = 0;
        public string DeliveryStatusName { get; set; } = "";
        public string PaymentStatusName { get; set; } = "";
    }
    public class OrderListModel
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

        public string OrderStatusName { get { return Convert.ToString((OrderStatus)Order_Status); } }
        public string PaymentStatusName { get { return Convert.ToString((PaymentStatus)Order_PaymentStatus); } }
    }
    public class OrderSortByModel
    {
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        public string DisplayName { get; set; }
    }
}
