using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entities
{
    [Table("Employee_Mas")]
    public class EmployeeMaster
    {
        [Key]
        public int EMP_Id { get; set; }
        public string EMP_FirstName { get; set; }
        public string EMP_LastName { get; set; }
        public int EMP_SellerId { get; set; }
        public string EMP_Address1 { get; set; }
        public string EMP_Address2 { get; set; }
        public int EMP_CityId { get; set; }
        public int EMP_StateId { get; set; }
        public string EMP_PostCode { get; set; }
        public string EMP_MobileNo { get; set; }
        public string EMP_EmailId { get; set; }
        public bool? EMP_Active { get; set; }
        public DateTime EMP_CreationDate { get; set; }
        public int EMP_CreatedBy { get; set; }
        public int? EMP_EditedBy { get; set; }
        public DateTime? EMP_EditedDate { get; set; }



    }
}
