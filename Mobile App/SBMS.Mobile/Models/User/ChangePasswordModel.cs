using System;
using System.Collections.Generic;
using System.Text;

namespace SBMS.Mobile.Models.User
{
    public class ChangePasswordResponseModel
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public string MobileNo { get; set; }
    }
    public class ChangePasswordModel
    {
        //public string OldPassword { get; set; }
        //public string NewPassword { get; set; }
        //public string ConfirmPassword { get; set; }
        //public string EmailId { get; set; }
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public bool ValidateRequest { get; set; } = true;


    }

    public class PasswordRecoveryModel
    {
        public string MobileNo { get; set; }
    }


}
