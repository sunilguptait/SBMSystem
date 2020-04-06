using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entities
{
    [Table("BookSeller_Mas")]
    public class BookSellerMaster
    {
        [Key]
        public int BSM_Id { get; set; }
        public string BSM_FirstName { get; set; }
        public string BSM_LastName { get; set; }
        public string BSM_FirmName { get; set; }
        public string BSM_RegistrationNo { get; set; }
        public string BSM_Address1 { get; set; }
        public string BSM_Address2 { get; set; }
        public int? BSM_CityId { get; set; }
        public int? BSM_StateId { get; set; }
        public string BSM_PostCode { get; set; }
        public string BSM_MobileNo { get; set; }
        public string BSM_EmailId { get; set; }
        public bool? BSM_Active { get; set; }
        public DateTime BSM_CreationDate { get; set; }
        public int? BSM_CreatedBy { get; set; }
        public int? BSM_EditedBy { get; set; }
        public DateTime? BSM_EditedDate { get; set; }
    }
}
