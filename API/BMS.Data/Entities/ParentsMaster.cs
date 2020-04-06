using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entities
{
    [Table("Parents_Mas")]
    public class ParentsMaster
    {
        [Key]
        public int P_Id { get; set; }
        public string P_Name { get; set; }
        public string P_Address1 { get; set; }
        public string P_Address2 { get; set; }
        public int P_CityId { get; set; }
        public int P_StateId { get; set; }
        public string P_PostCode { get; set; }
        public string P_MobileNo { get; set; }
        public string P_EmailId { get; set; }
        public bool P_Active { get; set; }
        public DateTime P_CreationDate { get; set; }
        public int P_CreatedBy { get; set; }
        public int? P_EditedBy { get; set; }
        public DateTime? P_EditedDate { get; set; }



    }
}
