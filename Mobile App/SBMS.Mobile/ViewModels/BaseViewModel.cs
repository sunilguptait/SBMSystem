using SBMS.Mobile.Services;
//using SBMS.Mobile.Views.Common;
using Plugin.Connectivity;
using Plugin.Share;
using Plugin.Share.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using SBMS.Mobile.Helpers;

namespace SBMS.Mobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        IMaterialModalPage materialDialog;
        public readonly IPageService _pageService;
        public ICommand SearchCommand { get; set; }
        public BaseViewModel()
        {
            _pageService = new PageService();
            SearchCommand = new Command(OnSearchClick);
        }
        public int PageSize { get; set; } = 20;
        public int FetchedRecords { get; set; } = -1;

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        bool hasNavigationBar = true;
        public bool HasNavigationBar
        {
            get { return hasNavigationBar; }
            set { SetProperty(ref hasNavigationBar, value); }
        }
        private bool _showLoader = false;
        public bool ShowLoader
        {
            get { return _showLoader; }
            set
            {
                if (value == true)
                {
                    //_pageService.ShowLoader().GetAwaiter().GetResult();
                }
                else
                {
                    materialDialog?.DismissAsync();
                }
                SetProperty(ref _showLoader, value);
            }
        }
        private bool _isListRefreshing = false;
        public bool IsListRefreshing
        {
            get { return _isListRefreshing; }
            set { SetProperty(ref _isListRefreshing, value); }
        }
        private bool _isShowEmptyView = false;
        public bool IsShowEmptyView
        {
            get { return _isShowEmptyView; }
            set { SetProperty(ref _isShowEmptyView, value); }
        }
        // To check that need to load more items in infinitelistview
        public bool IsFetchMore
        {
            get
            {
                if (FetchedRecords == -1 || FetchedRecords == PageSize)
                    return true;
                else
                    return false;
            }
        }
        private int _userTypeId = UserHelper.UserType;
        public int UserTypeId
        {
            get { return _userTypeId; }
            set { SetProperty(ref _userTypeId, value); }
        }
        private Color _cartBadgeColor = Color.Red;
        public Color CartBadgeColor
        {
            get { return _cartBadgeColor; }
            set { SetProperty(ref _cartBadgeColor, value); }
        }
        private Color _cartTextColor = Color.White;
        public Color CartTextColor
        {
            get { return _cartTextColor; }
            set { SetProperty(ref _cartTextColor, value); }
        }

        public bool CheckInternetConnection
        {
            get
            {
                if (!IsConnected)
                {
                    _pageService.ShowError("No Internet Connection!");
                    return true;
                }
                return false;
            }
        }
        public bool IsConnected
        {
            get
            {
                return CrossConnectivity.Current.IsConnected;
            }
        }
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Methods
        public void DisplayError(ObservableCollection<string> Errors)
        {
            if (!CheckInternetConnection)
            {
                if (Errors?.Count > 0)
                    _pageService.ShowSuccess(Errors[0]);
            }
        }
        public void DisplayError(string error)
        {
            if(error.Contains("401"))
            {
                _pageService.ShowError("You have logged out , please login again.");
                UserHelper.Logout();
            }
            if (!CheckInternetConnection)
            {
                _pageService.ShowError(error);
            }
        }
        public void DisplayMessage(string message)
        {
            _pageService.ShowSuccess(message);
        }

        public void ShareMessage(string txtMessage, string link = "", string title = "", string chooserTitle = "Share using")
        {
            ShareMessage message = new ShareMessage() { Text = txtMessage, Url = link };
            ShareOptions options = new ShareOptions() { ChooserTitle = chooserTitle };
            if (CrossShare.IsSupported)
                CrossShare.Current.Share(message, options);
        }
       
        private async void OnSearchClick()
        {
            //if (IsBusy)
            //    return;
            //IsBusy = true;
            //await _pageService.PushModelAsync(new Search());
            //IsBusy = false;

        }
        #endregion

    }
}
