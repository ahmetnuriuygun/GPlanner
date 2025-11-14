using GPlanner.Maui.ViewModels;

namespace GPlanner.Maui.Pages
{
    public partial class TaskModal : ContentPage
    {
        public TaskModal(TaskModalViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;

        }


    }
}
