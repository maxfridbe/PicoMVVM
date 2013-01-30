using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using PicoMVVM.Selectors;
using PicoMVVM.ViewModels;

namespace PicoMVVM.Commands
{
	public class ShowDialogCommand<TViewModelType> : ICommand where TViewModelType : DisplayableViewModelBase
	{
		private readonly TViewModelType _viewModel;
		private readonly ClassNameViewSelector _classNameViewSelector;

		public ShowDialogCommand(TViewModelType viewModel, ClassNameViewSelector viewSelector)
		{
			_viewModel = viewModel;
			_viewModel.PropertyChanged += _viewModel_PropertyChanged;
			_classNameViewSelector = viewSelector;
		}

		void _viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Displayable")
			{
				if (CanExecuteChanged != null)
					CanExecuteChanged(this, EventArgs.Empty);
			}
		}

		#region Implementation of ICommand

		public void Execute(object parameter)
		{
			var viewType = _classNameViewSelector.GetViewType(typeof(TViewModelType));
			var view = (Window)Activator.CreateInstance(viewType);
			view.DataContext = _viewModel;
			view.ShowDialog();
		}

		public bool CanExecute(object parameter)
		{
			return _viewModel.Displayable;
		}

		public event EventHandler CanExecuteChanged;

		#endregion
	}
}
