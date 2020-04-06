using SBMS.Mobile.Views.Order;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace SBMS.Mobile.ViewModels.BookSeller
{
    public class BookSellerHomePageViewModel : BaseViewModel
    {

        public ICommand ScanOrderNumberCommand { get; set; }
        public ICommand ViewOrdersCommand { get; set; }

        public BookSellerHomePageViewModel()
        {
            ScanOrderNumberCommand = new Command(OnScanOrderNumberClick);
            ViewOrdersCommand = new Command(OnViewOrdersClick);
        }
        public async void OnScanOrderNumberClick()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await ScanAsync();
            IsBusy = false;
        }
        public async void OnViewOrdersClick()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await _pageService.PushAsync(new SBMS.Mobile.Views.BookSeller.Orders());
            IsBusy = false;
        }

        public async Task ScanAsync()
        {

            var scanPage = new ZXingScannerPage();
            scanPage.Title = "Scan Order";
            scanPage.OnScanResult += (result) =>
            {
                // Stop scanning
                //scanPage.IsScanning = false;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(() =>
                {
                    _pageService.PopAsync();
                    _pageService.PushAsync(new ViewOrder(0,result.Text));
                });
            };
            // Navigate to our scanner page
            await _pageService.PushAsync(scanPage);

        }

    }
}
