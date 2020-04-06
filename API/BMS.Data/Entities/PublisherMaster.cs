using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entities
{
    [Table("Publisher_Mas")]
    public class PublisherMaster
    {
        [Key]
        public int Publisher_id { get; set; }
        public string Publisher_Name { get; set; }
        public string Publisher_Address { get; set; }
        public string Publisher_MobileNo { get; set; }
        public int Publisher_BSMId { get; set; }


    }
}
