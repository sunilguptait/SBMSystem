using BMS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.ViewModels.User
{
    public class BookSellerVM
    {
        public int? Id { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FirmName { get; set; }
        public string RegistrationNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public string PostCode { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public bool? Active { get; set; }
        public DateTime CreationDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
    }
    public class BookSellerSearchVM: BaseSearchModel
    {
        public string MobileNo { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
    }
}
