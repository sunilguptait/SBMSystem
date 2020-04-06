using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entities
{
    [Table("OrderDetail_Mas")]
    public class OrderDetailMaster
    {
        [Key]
        public int OD_Id { get; set; }
        public int? OD_OrderId { get; set; }
        public int? OD_BookId { get; set; }
        public decimal? OD_Price { get; set; }
        public int? OD_Qty { get; set; }
        public int? OD_Discount { get; set; }
        public decimal? OD_Total { get; set; }
        public DateTime? OD_CreationDate { get; set; }
        public int? OD_CreatedBy { get; set; }
        public bool? OD_IsDelete { get; set; }


    }
}
