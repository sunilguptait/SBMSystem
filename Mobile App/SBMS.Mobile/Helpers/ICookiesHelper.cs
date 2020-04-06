using System;
using System.Collections.Generic;
using System.Text;

namespace SBMS.Mobile.Helpers
{
    public interface ICookiesHelper
    {
        void Clear();
        string GetCookie(string url);
    }
}
