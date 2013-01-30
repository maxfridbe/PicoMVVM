using System;
using System.Windows;
using System.Windows.Controls;

namespace PicoMVVM.Selectors
{
	public abstract class ViewSelectorBase : DataTemplateSelector
	{
		public abstract Type GetViewType(Type itemType);
		
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			if (item == null)
				return new DataTemplate();

			var modelType = item.GetType();
			var viewType = GetViewType(modelType);

			var dt = new DataTemplate(viewType)
			         	{
			         		VisualTree = new FrameworkElementFactory(viewType)
			         	};

			return dt;
		}
	}
}