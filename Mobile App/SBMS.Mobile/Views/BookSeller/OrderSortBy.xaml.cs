using SBMS.Mobile.ViewModels.BookSeller;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SBMS.Mobile.Views.BookSeller
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderSortBy : StackLayout
    {
        public OrderSortBy()
        {
            InitializeComponent();
            BindingContext = new OrderSortByViewModel();
        }
    }
}