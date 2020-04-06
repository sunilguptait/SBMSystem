using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.ViewModels.User
{
    public class UserLoginVM
    {
        /// <summary>
        /// LoginType=1 Login By Email Id
        /// LoginType=2 Login By Mobile No
        /// </summary>
        public int LoginType { get; set; } = 1;
        /// <summary>
        /// LoginFrom=1 From Web
        /// LoginFrom=2 From Mobile
        /// </summary>
        public int LoginFrom { get; set; } = 1;
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class CreateUserVM
    {
        public string UserName { get; set; }
        public int UserType { get; set; }
        public int UserId { get; set; }
        public string Password { get; set; }
        
    }
    public class CreateUserResponseVM
    {
        public string Password { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string ErrorMessage { get; set; }
    }
}
