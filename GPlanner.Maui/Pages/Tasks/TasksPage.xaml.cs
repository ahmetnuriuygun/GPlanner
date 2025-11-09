using GPlanner.Maui.ViewModels;

namespace GPlanner.Maui.Pages
{
    public partial class TasksPage : ContentPage
    {
        public TasksPage(TasksViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}