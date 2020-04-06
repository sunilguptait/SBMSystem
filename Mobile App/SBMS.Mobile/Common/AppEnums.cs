using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMS.Mobile.Common
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
        [Description("Ordered")]
        Ordered = 1,
        [Description("Delivered")]
        Delivered = 2
    }
    public enum PaymentStatus
    {
        [Description("Pending")]
        Pending = 0,
        [Description("Paid")]
        Paid = 1
    }
    public enum PaymentMode
    {
        [Description("Cash at school")]
        CashAtSchool = 1,
        [Description("Debit Card")]
        DebitCard = 2,
        [Description("Credit Card")]
        CreditCard = 3,
        [Description("Cash")]
        Cash = 4,
        [Description("Card or wallet")]
        CardOrWallet = 5
    }
}
