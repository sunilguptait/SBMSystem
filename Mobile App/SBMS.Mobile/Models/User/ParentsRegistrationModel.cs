using System;
using System.Collections.Generic;
using System.Text;

namespace SBMS.Mobile.Models.User
{
    public class ParentsRegistrationModel
    {
        public int ParentsId { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
    }

    public class ValidateParentsUserNameModel
    {
        public string LoginName { get; set; }
        public int LoginType { get; set; }
    }
    public class ValidateParentsUserNameResponseModel
    {
        public int UserId { get; set; }
        public int ReturnValue { get; set; }
        public string ReturnMessage { get; set; }
    }

    public class OTPVerificationModel
    {
        public string MobileNo { get; set; }
        public string OTP { get; set; }
    }
    public class OTPVerificationResponseModel
    {
        public bool IsValid { get; set; }
    }
}