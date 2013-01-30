using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using PicoMVVM.ViewModels;

namespace PicoMVVM.Views
{
	public static class ViewSetter
	{
		public static EventHandler SetContext(IViewModel viewModel)
		{
			return (o,e) =>
			       	{
			       		(o as FrameworkElement).DataContext = viewModel;
			       	};
		}

	
	}
}
