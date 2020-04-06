using System;
using System.Collections.Generic;
using System.Text;

namespace SBMS.Mobile.Models.User
{
    public class LoginModel
    {
        /// <summary>
        /// LoginType=1 Login By Email Id
        /// LoginType=2 Login By Mobile No
        /// </summary>
        public int LoginType { get; set; } = 2;
        /// <summary>
        /// LoginFrom=1 From Web
        /// LoginFrom=2 From Mobile
        /// </summary>
        public int LoginFrom { get; set; } = 2;
        public string UserName { get; set; } 
        public string Password { get; set; } 
    }
}
