using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace PicoMVVM.Behaviors
{
	public static class DialogResultBehavior
	{
		public static readonly DependencyProperty DialogResultProperty =
			DependencyProperty.RegisterAttached(
				"DialogResult",
				typeof (bool?),
				typeof (DialogResultBehavior),
				new PropertyMetadata(default(bool?), OnDialogResultChanged));

		private static void OnDialogResultChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			var window = dependencyObject as Window;
			if (window == null)
				return;
			

			window.DialogResult = (bool?)e.NewValue;
		}
		public static void SetDialogResult(Window window, bool? value)
		{
			window.SetValue(DialogResultProperty, value);
		}

		/// <summary>
		/// Gets a window's dialog result.
		/// </summary>
		public static bool? GetDialogResult(Window window)
		{
			return (bool?)window.GetValue(DialogResultProperty);
		}
	}
}
