using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using XF.Material.Forms.UI;

namespace SBMS.Mobile.Common
{
    public static class ValidationExtensions
    {
        private static string emailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        private static string passwordRegex = @"^(?=.*?[a-z])(?=.*?[0-9]).{6,}$";
        private static string phoneRegex = @"[0-9]{10}";

        public static bool IsEmail(this string emailaddress)
        {
            return MatchPattren(emailRegex, emailaddress);
        }
        public static bool IsPassword(this string password)
        {
            return (MatchPattren(passwordRegex, password) && (!(password.Contains("&") || password.Contains("#"))));
        }
        public static bool IsPhoneNo(this string phoneNo)
        {
            if (phoneNo.Length != 10)
                return false;
            return MatchPattren(phoneRegex, phoneNo);
        }
        public static bool IsZipCode(this string zipcode)
        {
            if (zipcode.Length > 10)
                return false;
            return true;
        }
        public static bool MatchPassword(this string password, string confirmPassword)
        {
            return password.Equals(confirmPassword);
        }
        public static string Validate(this string txt, string fieldName, int max = 0, int min = 0)
        {
            string message = "";
            if (string.IsNullOrEmpty(txt))
                message = string.Format("{0} is required.", fieldName);
            else if (txt.Length > max)
                message = string.Format("{0} should not more than {1} characters.", fieldName, max);
            return message;
        }
        public static bool MatchPattren(string regexStr, string str)
        {
            if (str == null)
                return false;

            Regex regex = new Regex(regexStr);
            Match match = regex.Match(str);
            if (match.Success)
                return true;
            else
                return false;
        }
        public static bool ValidateMaterialTextFields(List<MaterialTextField> materialTextFields)
        {
            bool response = true;
            foreach (var field in materialTextFields)
            {
                bool isValid = ValidateMaterialTextField(field);
                if (isValid != true)
                    response = false;
            }
            return response;
        }

        public static bool ValidateMaterialTextField(MaterialTextField materialText)
        {
            bool response = true;
            // Check Empty Validation
            if (string.IsNullOrEmpty(materialText.Text))
            {
                materialText.HasError = true;
                response = false;
            }
            else
            {
                materialText.HasError = false;
            }
            //Check Email Validation
            if (materialText.InputType == MaterialTextFieldInputType.Email && response == true)
            {
                if (!materialText.Text.IsEmail())
                {
                    response = false;
                    materialText.HasError = true;
                    materialText.ErrorText = "Invalid email address";
                }
                else
                {
                    materialText.HasError = false;
                    materialText.ErrorText = "Email is required.";
                }
            }
            else if (materialText.InputType == MaterialTextFieldInputType.Telephone && response == true)
            {
                if (!materialText.Text.IsPhoneNo())
                {
                    response = false;
                    materialText.HasError = true;
                    materialText.ErrorText = "Invalid phone number";
                }
                else
                {
                    materialText.HasError = false;
                    materialText.ErrorText = "Phone is required.";
                }
            }
            return response;
        }
    }
}
