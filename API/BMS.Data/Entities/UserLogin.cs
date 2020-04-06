using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entities
{
    [Table("UserLogin")]
    public class UserLogin
    {
        [Key]
        public int UL_Id { get; set; }
        public string UL_LoginName { get; set; }
        public string UL_Password { get; set; }
        public int UL_Type { get; set; }
        public bool? UL_Active { get; set; }
        public DateTime? UL_CreationDate { get; set; }
        public int? UL_CreatedBy { get; set; }
        public DateTime? UL_EditedDate { get; set; }
        public int? UL_EditedBy { get; set; }
        public int UL_UserId { get; set; }
        public bool? UL_IsDefaultPassword { get; set; }

    }
}
