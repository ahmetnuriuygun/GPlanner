using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GPlanner.Core.Model;
using GPlanner.Maui.Interfaces;
using GPlanner.Maui.Services.Dtos;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace GPlanner.Maui.ViewModels
{
    public partial class TaskModalViewModel : ObservableObject
    {
        private readonly IUserTaskService _taskService;
        private readonly TasksViewModel _parentViewModel;

        [ObservableProperty]
        private bool isEditMode;

        [ObservableProperty]
        private UserTask activeTask = new UserTask();

        [ObservableProperty]
        private TimeSpan newTaskTime = DateTime.Now.TimeOfDay;

        [ObservableProperty]
        private DateTime minDate = DateTime.Now.Date;

        [ObservableProperty]
        private DateTime maxDate = DateTime.Now.Date.AddYears(1);

        public Array TaskTypes => Enum.GetValues(typeof(TaskType));

        [ObservableProperty]
        private int priority;
        public TaskModalViewModel(IUserTaskService taskService, TasksViewModel parentViewModel)
        {
            _taskService = taskService;
            _parentViewModel = parentViewModel;
            minDate = DateTime.Now.Date;
            maxDate = DateTime.Now.Date.AddYears(1);
        }

        public void Initialize(UserTask? taskToEdit)
        {
            if (taskToEdit != null)
            {
                IsEditMode = true;
                ActiveTask = taskToEdit;
                Priority = taskToEdit.Priority;
                NewTaskTime = taskToEdit.Date.TimeOfDay;
            }
            else
            {
                IsEditMode = false;
                ActiveTask = new UserTask { UserId = 1, Date = DateTime.Now.Date };
                Priority = 3;
                NewTaskTime = DateTime.Now.TimeOfDay;
            }
        }

        [RelayCommand]
        public async Task AddOrSaveAsync()
        {
            if (string.IsNullOrWhiteSpace(ActiveTask.Title))
            {
                await Shell.Current.DisplayAlert("Validation Error", "Please enter a task title.", "OK");
                return;
            }

            if (ActiveTask.Type == 0)
            {
                await Shell.Current.DisplayAlert("Validation Error", "Please select a task type.", "OK");
                return;
            }

            ActiveTask.Date = ActiveTask.Date.Date + NewTaskTime;

            bool success = false;
            string action = IsEditMode ? "updated" : "created";


            if (IsEditMode)
            {

                success = await _taskService.UpdateTaskAsync(ActiveTask);

            }
            else
            {
                ActiveTask.UserId = 1;
                success = await _taskService.CreateTaskAsync(ActiveTask);

            }

            if (success)
            {
                await _parentViewModel.LoadTasksAsync();

                string message = $"Task successfully {action}. To include this task in your daily schedule, please generate a new AI plan on the To Do page.";
                await Shell.Current.DisplayAlert("Success", message, "OK");

            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Task could not be saved. Check API connection.", "OK");
            }



            await Shell.Current.Navigation.PopModalAsync();
        }

        partial void OnPriorityChanged(int value)
        {
            if (ActiveTask != null)
                ActiveTask.Priority = value;
        }




    }
}