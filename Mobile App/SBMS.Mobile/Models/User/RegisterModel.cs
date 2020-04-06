﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SBMS.Mobile.Models.User
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string DOB { get; set; }
        public int Gender { get; set; }
        public string ReferralCode { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }

}
