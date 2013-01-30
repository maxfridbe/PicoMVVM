using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PicoMVVM.ViewModels
{
	public abstract class ViewModelBase : IViewModel, INotifyPropertyChanged
	{
		protected ViewModelBase()
		{

		}

		/// <see cref="INotifyPropertyChanged.PropertyChanged"/>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Raises the property changed event.
		/// </summary>
		/// <param name="propertyName">The name of the property that changed</param>
		protected virtual void OnPropertyChanged(string propertyName)
		{
			VerifyPropertyName(propertyName);

			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}


		/// <summary>
		/// Debug error if property name mismatch
		/// </summary>
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public void VerifyPropertyName(string propertyName)
		{
			if (TypeDescriptor.GetProperties(this)[propertyName] == null)
			{
				string msg = "Invalid property name: " + propertyName;
				Debug.Fail(msg);
			}
		}

		//public TServiceContract GetService<TServiceContract>()
		//    where TServiceContract : class
		//{
		//    return ServiceContainer.Instance.GetService<TServiceContract>();
		//}
	}

}
