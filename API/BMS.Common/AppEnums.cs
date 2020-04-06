using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Common
{
    public enum CacheKeys
    {

    }
    public enum UserTypes
    {
        Admin = 1,
        BookSeller,
        Parents
    }
    public enum OrderStatus
    {
        Ordered = 1,
        Delivered = 2
    }
    public enum PaymentStatus
    {
        Pending = 0,
        Paid = 1
    }
    public enum PaymentMode
    {
        CashAtSchool = 1,
        DebitCard = 2,
        CreditCart = 3,
        Cash = 4,
        CardOrWallet = 5
    }
}
