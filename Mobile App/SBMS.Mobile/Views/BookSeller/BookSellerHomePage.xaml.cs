using Autofac;
using SBMS.Mobile.ViewModels.BookSeller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SBMS.Mobile.Views.BookSeller
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookSellerHomePage : ContentPage
    {
        BookSellerHomePageViewModel _vm = null;
        public BookSellerHomePage()
        {
            if (_vm == null)
            {
                _vm = App.Container.Resolve<BookSellerHomePageViewModel>();
                InitializeComponent();
                BindingContext = _vm;
            }
        }
    }
}