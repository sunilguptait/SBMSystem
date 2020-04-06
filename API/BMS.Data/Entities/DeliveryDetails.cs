using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entities
{
    [Table("Delivery_Detail")]
    public class DeliveryDetails
    {
        [Key]
        public int D_Id { get; set; }
        public int D_OrderId { get; set; }
        public DateTime D_Date { get; set; }
        public string D_ReceviedBy { get; set; }
        public string D_Signature { get; set; }
        

    }
}
