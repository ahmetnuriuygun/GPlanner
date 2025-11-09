namespace GPlanner.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(Pages.TaskModal), typeof(Pages.TaskModal));
	}
}
