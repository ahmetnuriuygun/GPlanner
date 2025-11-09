using GPlanner.Maui.ViewModels;

namespace GPlanner.Maui.Pages
{
	public partial class UserPage : ContentPage
	{
		public UserPage(UserViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = viewModel;
		}
	}
}

