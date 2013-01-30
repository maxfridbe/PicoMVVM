PicoMVVM
========

A small MVVM framework meant to be integrated into your project (not precompiled).

Problem:
	Most MVVM Frameworks try to do too much.  You end up fighting them in order to get them to do what you want them to.

Solution:
	Create a .csproj that can be integrated with anyone starting a WPF MVVM project.  This allows the person to add more functionality (than is currently supported in PicoMVVM) and also allows pruning of features.

How to use:
>  PicoMVVM has one dependency that must be installed. 
	We also reccommend using this dependency for your own project. 
	Add the VS plugin Fody.  
	This is a codeweaver extension which keeps you from having to write manual NotifyPropertyChanged. 
	_Note:_ it is trivial to change PicoMVVM to not use Fody so feel free to do so if you are against codeweaving.

1.  In the XAML (of each view that has to load a dynamic view):
	```xaml
	<Window.Resources>
	     <ResourceDictionary>
	          <Selectors:ClassNameViewSelector x:Key="ViewSelector" />
	     </ResourceDictionary>
	</Window.Resources>
    
	<ContentControl Name="ctrlContentCenter" 
        		Content="{Binding CurrentViewModel}"  
        		ContentTemplateSelector="{StaticResource ViewSelector}"
                />
	```
	> __Note:__ you can pick whichever Selectors:x that you want however the only one supplied by default is a Convention of ClassNameViewSelector. This selector requires the naming convention of your view to be xxxxxxView.xaml, and the viewmodel to be xxxxxxViewModel.cs.  If you want different conventions just subclass ViewSelectorBase

2.  The _ONLY_ bit of sourcery is a one time setting of the DataContext of the Initializing View:
	```cs
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			//this only has to be done each time there is a root xaml node
			this.Initialized += (a, b) => 
				DataContext = TinyIoCContainer.Current.Resolve<MainWindowViewModel>();
			InitializeComponent();
		}
	}
	```

3.  In the ViewModel source:
	a.  If your view contains other views subclass ContainerViewModelBase. Setting CurrentViewModel will trigger the XAML binding to pick an appropriate view for your ViewModel
	b.  If your view stands alone subclass ViewModelBase

Other Features
--------------
In order to do better MVVM some other features have been added

*  MessageCenter (basic pub-sub):
	```cs

	//Allows components to send messages to one another
	var messageCenter = new MessageCenter();
	messageCenter.Subscribe<message>(a => { res = a.txt; }, "topic");
	messageCenter.Publish(msg, "topic");


	```

*  Commands
	a.  ShowDialogCommand, a command which knows a bit about the View and ViewModel and can open a dialog.
	b.  RelayCommand, simple lambda command interface

*  Collections
	a.  AsyncObservableCollection, a collection which allows adding via other threads but will notify on UI thread.  _Note:_The collection must be initialized on the UI thread.
