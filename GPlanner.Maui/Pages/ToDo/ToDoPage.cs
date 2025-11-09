using GPlanner.Maui.ViewModels;

namespace GPlanner.Maui.Pages
{
	public partial class ToDoPage : ContentPage
	{
		public ToDoPage(ToDoViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = viewModel;
		}
	}
}


