using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SBMS.Mobile.Models.User
{
   public class LoginResponseModel
    {
        public int UserId { get; set; }
        public int UserType { get; set; }
        public int BookSellerId { get; set; }
        public int ParentsId { get; set; }
        public string UserDisplayName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
