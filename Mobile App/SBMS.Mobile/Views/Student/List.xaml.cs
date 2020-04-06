using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using SBMS.Mobile.Models.Student;
using SBMS.Mobile.ViewModels.Student;

namespace SBMS.Mobile.Views.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class List : StackLayout
    {
        public StudentViewModel _vm = null;
        public List()
        {
            if (_vm == null)
            {
                _vm = App.Container.Resolve<StudentViewModel>();
                BindingContext = _vm;
                _vm.LoadData();
            }
            InitializeComponent();
        }
       
        protected async void OnListRefresh(object sender, System.EventArgs e)
        {
            _vm.List = new ObservableCollection<StudentModel>();
            await _vm.LoadData();
            //OrdersList.EndRefresh();
        }
    }
}