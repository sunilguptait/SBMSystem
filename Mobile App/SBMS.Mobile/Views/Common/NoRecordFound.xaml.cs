using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SBMS.Mobile.Views.Common
{
    public partial class NoRecordFound : StackLayout
    {
        private StackLayout _emptyTemplate;
        public NoRecordFound()
        {
            InitializeComponent();
        }
        public virtual StackLayout EmptyTemplate
        {
            get { return _emptyTemplate; }
            set
            {
                _emptyTemplate = value;
                _emptyTemplate.HorizontalOptions = LayoutOptions.StartAndExpand;
                EmptyTemplateStack.Children.Insert(0, _emptyTemplate);

                OnPropertyChanged();
            }
        }
    }
}
