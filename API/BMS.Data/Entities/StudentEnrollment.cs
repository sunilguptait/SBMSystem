using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entities
{
    [Table("Student_Enrollment")]
    public class StudentEnrollment
    {
        [Key]
        public int SE_Id { get; set; }
        public int? SE_StudentId { get; set; }
        public int? SE_ClassId { get; set; }
        public int? SE_ParentId { get; set; }
        public int? SE_SessionId { get; set; }
        public DateTime? SE_CreationDate { get; set; }
        public int? SE_CreatedBy { get; set; }
        public DateTime? SE_EditedDate { get; set; }
        public int? SE_EditedBy { get; set; }


    }
}
