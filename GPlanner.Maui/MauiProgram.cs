using Microsoft.Extensions.Logging;
using GPlanner.Maui.Pages;
using GPlanner.Maui.ViewModels;
using GPlanner.Maui.Interfaces;
using GPlanner.Maui.Mapping;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace GPlanner.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<IUserTaskService, UserTaskService>();
		builder.Services.AddAutoMapper(typeof(MappingProfile));

		builder.Services.AddTransient<ToDoPage>();
		builder.Services.AddTransient<UserPage>();
		builder.Services.AddTransient<TasksPage>();
		builder.Services.AddTransient<TaskModal>();

		builder.Services.AddTransient<ToDoViewModel>();
		builder.Services.AddTransient<UserViewModel>();
		builder.Services.AddTransient<TasksViewModel>();
		builder.Services.AddTransient<TaskModalViewModel>();


		builder.Services.AddTransient<Func<TasksViewModel, TaskModalViewModel>>(sp =>
			(parentVm) => new TaskModalViewModel(
				sp.GetRequiredService<IUserTaskService>(),
				sp.GetRequiredService<IMapper>(),
				parentVm
			));

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}