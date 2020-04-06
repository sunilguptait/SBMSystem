using SBMS.Mobile.ViewModels.User;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI;
using static SBMS.Mobile.Common.ValidationExtensions;

namespace SBMS.Mobile.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEdit : ContentPage
    {
        AddEditStudentViewModel _vm = null;
        int StudentId = 0;
        public AddEdit(int studentId = 0)
        {
            Title = studentId > 0 ? "Edit Student" : "Add New Student";
            StudentId = studentId;
            InitializeComponent();
           
        }
        protected override async void OnAppearing()
        {
            try
            {
                if (_vm == null)
                {
                    _vm = App.Container.Resolve<AddEditStudentViewModel>();
                    _vm.StudentId = StudentId;
                    _vm.InitilizeModel();
                    BindingContext = _vm;
                    
                    //if (StudentId > 0)
                    //{
                    //    School.IsEnabled = false;
                    //    Class.IsEnabled = false;
                    //}
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            if (ValidatePage())
            {
                _vm.SaveCommand.Execute(null);
            }
        }
        private bool ValidatePage()
        {
            bool isValid = ValidateMaterialTextFields(new System.Collections.Generic.List<XF.Material.Forms.UI.MaterialTextField>()
            { StudentName, EnrollmentNo,Class,School });
            if (DOB.Date == null)
            {
                DOB.HasError = true;
                isValid = false;
            }
            else
            {
                DOB.HasError = false;
            }

            return isValid;
        }


    }
}