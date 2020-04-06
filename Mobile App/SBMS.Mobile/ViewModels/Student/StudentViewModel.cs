
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

using System.Linq;
using SBMS.Mobile.ViewModels;
using SBMS.Mobile.Services.Student;
using SBMS.Mobile.Models.Common;
using SBMS.Mobile.Common;
using SBMS.Mobile.Models;
using SBMS.Mobile.Models.Student;
using SBMS.Mobile.Views.User;
using XF.Material.Forms.Models;
using SBMS.Mobile.Views.Order;
using SBMS.Mobile.Services;
using ZXing.Net.Mobile.Forms;

namespace SBMS.Mobile.ViewModels.Student
{
    public class StudentViewModel : BaseViewModel
    {
        private readonly IStudentService _studentService;

        public ICommand LoadListDataCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand StudentSelectCommand { get; set; }
        public ICommand AddNewStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand BuyBookCommand { get; set; }


        private ObservableCollection<StudentModel> _list;
        public ObservableCollection<StudentModel> List
        {
            get { return _list; }
            set { SetProperty(ref _list, value); }
        }
        private StudentModel _selectedItem;
        public StudentModel SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
        public StudentViewModel(IStudentService studentService)
        {
            _studentService = studentService;
            LoadListDataCommand = new Command<int>(async vm => LoadListData(vm));
            StudentSelectCommand = new Command(OnStudentSelect);
            AddNewStudentCommand = new Command(OnAddNewStudentClick);
            EditStudentCommand = new Command<int>(OnEditStudentClick);
            BuyBookCommand = new Command<int>(OnBuyBookClick);
            RefreshCommand = new Command(async vm => OnRefreshClick());
            //LoadData();
        }
        public async Task LoadData()
        {
            await LoadListData();
        }
        public async Task OnRefreshClick()
        {
            FetchedRecords = -1;
            List = new ObservableCollection<StudentModel>();
            await LoadListData();
        }

        public async Task LoadListData(int CallFrom = 0)
        {
            if (!IsFetchMore)
                return;

            if (List == null || CallFrom == 0)
                List = new ObservableCollection<StudentModel>();

            ShowLoader = true;

            var response = await _studentService.GetStudents(new StudentSearchModel()
            { PageSize = PageSize, PageIndex = List.PageIndex(PageSize) });
            if (!response.Success)
            {
                ShowLoader = false;
                DisplayError(response.ErrorMessage);
                return;
            }

            // Model = response.Data;
            NormanizeList(response);
            ShowLoader = false;
        }
        void NormanizeList(ApiBaseModel<ObservableCollection<StudentModel>> response)
        {
            if (response?.Data == null)
                return;

            foreach (var item in response?.Data)
            {
                List.Add(item);
            }
            FetchedRecords = Convert.ToInt32(response?.Data.Count);
            IsShowEmptyView = List.Count > 0 ? false : true;
        }
        public async void OnStudentSelect()
        {
            if (SelectedItem == null)
                return;

            if (IsBusy)
                return;
            IsBusy = true;
            int orderId = Convert.ToInt32(SelectedItem?.Id);
            // await _pageService.PushAsync(new CommonWebView($"{App.GetPropertyValue("DMSApiBaseURL")}/orderdetails/29?isMobileClient=true", "Order Details"));
            SelectedItem = null;
            IsBusy = false;
        }

        public async void OnAddNewStudentClick()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await _pageService.PushAsync(new AddEdit());
            SelectedItem = null;
            IsBusy = false;
        }
        public async void OnEditStudentClick(int studentId)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await _pageService.PushAsync(new AddEdit(studentId));
            IsBusy = false;
        }
        public async void OnBuyBookClick(int studentId)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            var studentRow = List.Where(a => a.Id == studentId).FirstOrDefault();
            if (studentRow != null)
            {
                if (!Convert.ToBoolean(studentRow.IsBooksPurchased))
                {
                    await _pageService.PushAsync(new BuyBooks(studentRow));
                }
                else
                {
                    await _pageService.PushAsync(new ViewOrder(0, studentRow.OrderCode));
                }
            }
            IsBusy = false;
        }

    }
}
