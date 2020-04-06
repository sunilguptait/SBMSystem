using BMS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.ViewModels
{
    public class BaseViewModel
    {
        public int UserId { get; set; } = SessionManager.UserId;
        public int UserTypeId { get; set; } = SessionManager.UserTypeId;
        public int BookSellerId { get; set; } = SessionManager.BookSellerId;
        public int ParentsId { get; set; } = SessionManager.ParentsId;

    }
}
