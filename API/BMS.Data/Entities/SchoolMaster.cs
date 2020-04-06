using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entities
{
    [Table("School_Mas")]
    public class SchoolMaster
    {
        [Key]
        public int SM_Id { get; set; }
        public string SM_Name { get; set; }
        public string SM_Address { get; set; }
        public string SM_MobileNo { get; set; }
        public bool? IsActive { get; set; }

    }
}
