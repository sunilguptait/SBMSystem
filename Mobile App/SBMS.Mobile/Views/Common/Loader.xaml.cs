﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SBMS.Mobile.Views.Common
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Loader : StackLayout
    {
		public Loader ()
		{
			InitializeComponent ();
		}
	}
}