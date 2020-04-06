using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entities
{
    [Table("Order_Mas")]
    public class OrderMaster
    {
        [Key]
        public int Order_Id { get; set; }
        public string Order_Code { get; set; }
        public DateTime? Order_date { get; set; }
        public int? Order_SEId { get; set; }
        public int? Order_Status { get; set; }
        public int? Order_PaymentMode { get; set; }
        public int? Order_PaymentStatus { get; set; }
        public decimal? Order_TotalAmount { get; set; }
        public int? Order_BSId { get; set; }
        public string Order_PaymentRemark { get; set; }
        public string Order_ReceiverSignature { get; set; }
        public string Order_QRCode { get; set; }
    }
}
