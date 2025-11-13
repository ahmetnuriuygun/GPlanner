using GPlanner.Maui.ViewModels;

namespace GPlanner.Maui.Pages
{
	public partial class UserPage : ContentPage
	{
		private readonly UserViewModel _viewModel;
		public UserPage(UserViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = _viewModel = viewModel;

		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await _viewModel.LoadUserAsync();
		}
	}
}

