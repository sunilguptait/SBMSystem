using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMS.Mobile.Helpers
{
    public static class MessageHelper
    {
        public static string AllowLocation { get { return "Please Allow Your Location to Proceed Further."; } }
        public static string NeedLogin { get { return "Please Login to Proceed Further."; } }
        public static string ForgetPassword { get { return "forgetpassword"; } }
        public static string NewAppVersionMessage { get { return "Thank You for continuing with Kayawell!! A new APP version with lot of changes is available now. Please download the update from the Play Store for having a better experience and accessing new features. Do you want to download the update?"; } }
        public static string NewAppVersionPageDescription { get { return "Thank You for continuing with Kayawell!! A new APP version with lot of changes is available now. Please update the current version of APP for having a better experience and accessing new features."; } }
        public static string CommonErrorMessage { get { return "Problem in Connecting. Please Try Again!!"; } }
        public static string PasswordValidationMessage { get { return "Password must have minimum six characters, at least one letter and one number. (Excluding & and #)"; } }
        public static string OTPSentSuccessfully { get { return "OTP has been sent successfully"; } }
    }

    public static class ShareMessageHelper
    {
        //public static string AppShareText { get { return "Kayawell – A Total Healthcare Solution."; } }
        //public static string ExpertShareText { get { return "Kayawell – A Total Healthcare Solution."; } }
        //public static string PlanShareText { get { return "Kayawell – A Total Healthcare Solution."; } }
        //public static string EventShareText { get { return "Kayawell – A Total Healthcare Solution."; } }
    }

    public static class ErrorMessageHepler
    {
        public static string Common { get { return "Something went wrong. Please try again"; } }
    }

}
