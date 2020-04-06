using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.ViewModels.User
{
    public class UserLoginResponseModel
    {
        public int UserId { get; set; }
        public int UserType { get; set; }
        public int BookSellerId { get; set; }
        public int ParentsId { get; set; }
        public string UserDisplayName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int LoginFrom { get; set; }
        public List<SchoolVM> Schools { get; set; }
        public bool? IsDefaultPassword { get; set; }
    }
}
