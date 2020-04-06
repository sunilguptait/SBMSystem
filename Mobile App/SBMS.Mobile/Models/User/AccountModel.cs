using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SBMS.Mobile.Models.User
{
    public class AccountModel
    {
        public string UserName { get; set; }
        public string UserImage { get; set; }
    }
    public class MenuModel
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        //public string ItemType { get; set; }
        public Page PageName { get; set; }
    }
}
