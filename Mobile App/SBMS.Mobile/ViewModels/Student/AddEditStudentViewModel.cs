using SBMS.Mobile.Helpers;
using SBMS.Mobile.Models.Student;
using SBMS.Mobile.Models.User;
using SBMS.Mobile.Services;
using SBMS.Mobile.Services.Student;
using SBMS.Mobile.Services.User;
using SBMS.Mobile.Views.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using SBMS.Mobile.Models.Common;
using Xamarin.Forms;
using SBMS.Mobile.Services.Common;
using System.Linq;
using SBMS.Mobile.Common;
namespace SBMS.Mobile.ViewModels.User
{
    public class AddEditStudentViewModel : BaseViewModel
    {
        public ICommand SaveCommand { get; set; }

        private readonly IStudentService _studentService;
        private readonly ICommonService _commonService;

        private StudentModel model;
        public StudentModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }
      
        private ObservableCollection<DropdownModel> classList;
        public ObservableCollection<DropdownModel> ClassList
        {
            get { return classList; }
            set { SetProperty(ref classList, value); }
        }
        private ObservableCollection<DropdownModel> schoolsList;
        public ObservableCollection<DropdownModel> SchoolsList
        {
            get { return schoolsList; }
            set { SetProperty(ref schoolsList, value); }
        }
        public int StudentId { get; set; }
        public AddEditStudentViewModel(IStudentService studentService, ICommonService commonService)
        {
            _studentService = studentService;
            _commonService = commonService;
            SaveCommand = new Command(OnSave);
            //InitilizeModel();

        }
        public void InitilizeModel()
        {
            Model = new StudentModel();
            if (StudentId > 0)
            {
                GetStudentData();
            }
            ClassList.Initialize();
            SchoolsList.Initialize();
            GetClasses();
            GetSchools();
        }
        public async void GetStudentData()
        {
            await _pageService.ShowLoader();
            var response = await _studentService.GetStudentById(StudentId);
            _pageService.HideLoader();
            if (!response.Success)
            {
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                    DisplayError(response.ErrorMessage);
                IsBusy = false;
                return;
            }
            Model = response.Data;
        }
        public async void GetClasses()
        {
            var response = await _commonService.GetClassDropdown();
            if (!response.Success)
            {
                DisplayError(response.ErrorMessage);
                return;
            }
            ClassList = response.Data;
        }
        public async void GetSchools()
        {
            var response = await _commonService.GetSchoolDropdown();
            if (!response.Success)
            {
                DisplayError(response.ErrorMessage);
                return;
            }
            SchoolsList = response.Data;
        }

        public async void OnSave()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await _pageService.ShowLoader();
            BindDropdownValuesInModel();
            var response = await _studentService.SaveStudent(Model);
            if (!response.Success)
            {
                _pageService.HideLoader();
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                    DisplayError(response.ErrorMessage);
                IsBusy = false;
                return;
            }
            DisplayMessage("Data Saved Scuccessfully");
            _pageService.HideLoader();
            IsBusy = false;
            MessagingCenter.Send<AddEditStudentViewModel>(this, "RefreshStudentsListAfterAddEdit");
            await _pageService.PopAsync();
        }
        void BindDropdownValuesInModel()
        {
            Model.ParentId = UserHelper.UserId;
            var selectedSchool = SchoolsList.Where(a => a.Text == Model.SchoolName).FirstOrDefault();
            if (selectedSchool != null)
            {
                Model.SchoolId = selectedSchool.Value;
            }

            var selectedClass = ClassList.Where(a => a.Text == Model.ClassName).FirstOrDefault();
            if (selectedClass != null)
            {
                Model.ClassId = selectedClass.Value;
            }
        }
    }
}
