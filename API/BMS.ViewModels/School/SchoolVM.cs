using BMS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.ViewModels.User
{
    public class SchoolVM
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
    }
    public class SchoolSearchVM : BaseSearchModel
    {
        public string MobileNo { get; set; }
        public string Name { get; set; }
    }
}
