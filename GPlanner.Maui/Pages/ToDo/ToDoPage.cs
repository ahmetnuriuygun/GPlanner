using GPlanner.Maui.ViewModels;

namespace GPlanner.Maui.Pages
{
	public partial class ToDoPage : ContentPage
	{
		private readonly ToDoViewModel _viewModel;

		public ToDoPage(ToDoViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = _viewModel = viewModel;
		}


		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await _viewModel.LoadDailyPlansAsync();
		}
	}
}


