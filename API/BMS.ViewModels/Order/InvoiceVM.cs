using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.ViewModels.Order
{
    public class InvoiceInputVM : BaseViewModel
    {
        public List<string> Orders { get; set; }
        public int SessionId { get; set; }
    }

    public class InvoiceVM
    {
        public string BookSellerFirmName { get; set; }
        public string BookSellerAddress1 { get; set; }
        public string BookSellerAddress2 { get; set; }
        public string BookSellerCity { get; set; }
        public string BookSellerState { get; set; }
        public string BookSellerPostCode { get; set; }
        public string BookSellerEmail { get; set; }
        public string BookSellerMobile { get; set; }
        public string StudentName { get; set; }
        public string StudentAddress { get; set; }
        public string ParentName { get; set; }
        public string ParentAddress1 { get; set; }
        public string ParentAddress2 { get; set; }
        public string ParentCity { get; set; }
        public string ParentState { get; set; }
        public string ParentPostCode { get; set; }
        public string ParentEmail { get; set; }
        public string ParentMobile { get; set; }
        public string BookName { get; set; }
        public decimal BookPrice { get; set; }
        public int BookQty { get; set; }
        public decimal BookAmount { get; set; }
        public int BookDiscount { get; set; }
        public decimal OrderAmount { get; set; }
        public string OrderCode { get; set; }
        public int OrderId { get; set; }
        public string OrderDate { get; set; }
    }
}
