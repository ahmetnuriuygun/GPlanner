using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GPlanner.Core.Model;
using GPlanner.Maui.Services.Dtos;
using GPlanner.Maui.Interfaces;
using AutoMapper;
using GPlanner.Maui.Pages;
using System.Collections.Generic;

namespace GPlanner.Maui.ViewModels
{
    public partial class TasksViewModel : ObservableObject
    {
        private readonly IUserTaskService _taskService;
        private readonly Func<TasksViewModel, TaskModalViewModel> _taskModalVmFactory;

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        ObservableCollection<UserTask> tasks = new();

        [ObservableProperty]
        private bool isBusy;
        [ObservableProperty]
        ObservableCollection<UserTask> filteredTasks = new();

        public Array TaskTypes => Enum.GetValues(typeof(TaskType));



        public TasksViewModel(IUserTaskService taskService, Func<TasksViewModel, TaskModalViewModel> taskModalVmFactory)
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _taskModalVmFactory = taskModalVmFactory ?? throw new ArgumentNullException(nameof(taskModalVmFactory));

            Task.Run(async () => await LoadTasksAsync());
        }

        [RelayCommand]
        public async Task LoadTasksAsync()
        {
            if (IsBusy) return;
            try
            {
                int loggedInUserId = 1;
                var taskList = await _taskService.GetTasksByUserIdAsync(loggedInUserId);
                var notArchivedTasks = taskList.Where(t => !t.IsArchived).ToList();
                Tasks = new ObservableCollection<UserTask>(taskList);
                OnSearchTextChanged(SearchText);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadTasksAsync Error: {ex}");
            }
            finally
            {
                IsBusy = false;
            }


        }

        partial void OnSearchTextChanged(string value)
        {
            if (Tasks == null) return;

            if (string.IsNullOrWhiteSpace(value))
            {
                FilteredTasks = new ObservableCollection<UserTask>(Tasks);
            }
            else
            {
                var results = Tasks
                    .Where(t => t.Title.Contains(value, StringComparison.OrdinalIgnoreCase));
                FilteredTasks = new ObservableCollection<UserTask>(results);
            }
        }

        [RelayCommand]
        public async Task OpenTaskModal(UserTask? taskToEdit = null)
        {
            var vm = _taskModalVmFactory(this);

            vm.Initialize(taskToEdit);

            var modal = new TaskModal(vm);
            await Shell.Current.Navigation.PushAsync(modal);
        }

        [RelayCommand]
        public async Task Delete(UserTask task)
        {
            bool confirm = await Shell.Current.DisplayAlert("Delete Task", "Are you sure you want to delete this task?", "Yes", "No");
            if (!confirm) return;
            if (confirm)
            {
                bool success = await _taskService.DeleteTaskAsync(task.TaskId);
                if (success)
                {
                    await Shell.Current.DisplayAlert("Success", "Task deleted successfully.To include this task in your daily schedule, please generate a new AI plan on the To Do page.", "OK");
                    await LoadTasksAsync();
                }

            }
        }

        [RelayCommand]
        public async Task Archive(UserTask task)
        {
            bool confirm = await Shell.Current.DisplayAlert("Archive Task", "Are you sure you want to archive this task?", "Yes", "No");
            if (!confirm) return;
            else
            {
                bool success = await _taskService.ArchiveTaskAsync(task.TaskId);
                if (success)
                {
                    await Shell.Current.DisplayAlert("Success", "Task archived successfully.To include this task in your daily schedule, please generate a new AI plan on the To Do page.", "OK");
                    await LoadTasksAsync();
                }

            }
        }


    }
}