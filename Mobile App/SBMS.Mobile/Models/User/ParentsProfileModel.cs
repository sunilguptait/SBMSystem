using System;
using System.Collections.Generic;
using System.Text;

namespace SBMS.Mobile.Models.User
{
    public class ParentsProfileModel
    {
        public int ParentsId { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostCode { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
}