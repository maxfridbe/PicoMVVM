using System.ComponentModel;

namespace PicoMVVM.ViewModels
{
	public interface IContainerViewModel : INotifyPropertyChanged, IViewModel
	{
		IViewModel CurrentViewModel { get; set; }
	}
}