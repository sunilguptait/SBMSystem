using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entities
{
    [Table("Student_Mas")]
    public class StudentMaster
    {
        [Key]
        public int St_id { get; set; }
        public string St_Name { get; set; }
        public int? St_SchoolId { get; set; }
        public int? St_Address { get; set; }
        public DateTime? St_DateOfBirth { get; set; }
        public string St_EnrollmentNo { get; set; }
        public int? St_ParentId { get; set; }

    }
}
