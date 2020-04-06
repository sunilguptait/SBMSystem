using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SBMS.Mobile.Views.Common
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopUp : PopupPage
	{
		public PopUp (StackLayout popupContent,bool CloseWhenBackgroundIsClicked=true)
		{
			InitializeComponent ();
            this.CloseWhenBackgroundIsClicked = true;
            MainStack.Children.Add(popupContent);
		}
		protected override bool OnBackgroundClicked()
		{
			//Navigation.PopPopupAsync();
			return false;
		}
	}
}