using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entities
{
    [Table("SellerSchool_Mapping")]
    public class SellerSchoolMapping
    {
        [Key]
        public int SSM_Id { get; set; }
        public int SSM_BSMId { get; set; }
        public int SSM_SMId { get; set; }
        public DateTime SSM_CreationDate { get; set; }
        public int SSM_CreatedBy { get; set; }
        public DateTime? SSM_EditedDate { get; set; }
        public int? SSM_EditedBy { get; set; }
        public bool SSM_IsDeleted { get; set; }

    }
}
