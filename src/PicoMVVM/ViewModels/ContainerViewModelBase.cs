namespace PicoMVVM.ViewModels
{
	public class ContainerViewModelBase :  ViewModelBase, IContainerViewModel
	{
		public ContainerViewModelBase()
		{
		}

		#region Implementation of IContainerViewModel

		private IViewModel _currentViewModel;
		public IViewModel CurrentViewModel
		{
			get { return _currentViewModel; }
			set
			{
				if (_currentViewModel == value) return;

				
				_currentViewModel = value;
				OnPropertyChanged("CurrentViewModel");
			}
		}

		#endregion

		
	}
}