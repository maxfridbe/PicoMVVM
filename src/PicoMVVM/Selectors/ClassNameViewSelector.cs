using System;
using System.Linq;

namespace PicoMVVM.Selectors
{
	/// <summary>
	/// <example>
	/// xmlns:Selectors="clr-namespace:PicoMVVM.Selectors;assembly=TinyMVVM"
	///		<Window.Resources>
	///			<Selectors:ClassNameViewSelector x:Key="ClassNameViewSelector"/>
	///		</Window.Resources>
	///		<DockPanel Name="pnlDockMain" >
	///			<ContentControl Name="ctrlContentCenter" 
	///							Content="{Binding CurrentViewModel}"  
	///							ContentTemplateSelector="{StaticResource ClassNameViewSelector}" />
	///		</DockPanel>
	/// </example>
	/// </summary>
	public class ClassNameViewSelector : ViewSelectorBase
	{
		public override Type GetViewType(Type itemType)
		{
			//figure out the view type name
			var viewTypeName = itemType.Name.Replace("Model", string.Empty);

			//the view should be in the same assembly as the view model
			//if not replace with better logic (ioc?)
			var viewType = itemType.Assembly.GetTypes().Single(t => t.Name == viewTypeName);
			return viewType;
		}
	
	}
}
