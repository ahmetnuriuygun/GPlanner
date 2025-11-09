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
        private readonly IMapper _mapper;
        private readonly Func<TasksViewModel, TaskModalViewModel> _taskModalVmFactory;

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        ObservableCollection<TaskItemViewModel> tasks = new();

        [ObservableProperty]
        ObservableCollection<TaskItemViewModel> filteredTasks = new();

        public Array TaskTypes => Enum.GetValues(typeof(TaskType));

        public TasksViewModel(IUserTaskService taskService, IMapper mapper, Func<TasksViewModel, TaskModalViewModel> taskModalVmFactory)
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _taskModalVmFactory = taskModalVmFactory ?? throw new ArgumentNullException(nameof(taskModalVmFactory));

            Task.Run(async () => await LoadTasksAsync());
        }

        [RelayCommand]
        public async Task LoadTasksAsync()
        {
            int loggedInUserId = 1;
            var dtoList = await _taskService.GetTasksByUserIdAsync(loggedInUserId);

            var mapped = _mapper.Map<List<UserTaskReadDto>>(dtoList);

            var wrapped = mapped.Select(dto => new TaskItemViewModel(dto, this));
            Tasks = new ObservableCollection<TaskItemViewModel>(wrapped);

            OnSearchTextChanged(SearchText);
        }

        partial void OnSearchTextChanged(string value)
        {
            if (Tasks == null) return;

            if (string.IsNullOrWhiteSpace(value))
                FilteredTasks = new ObservableCollection<TaskItemViewModel>(Tasks);
            else
            {
                var results = Tasks
                    .Where(t => t.Title.Contains(value, StringComparison.OrdinalIgnoreCase));
                FilteredTasks = new ObservableCollection<TaskItemViewModel>(results);
            }
        }

        [RelayCommand]
        public async Task OpenTaskModal(TaskItemViewModel? task = null)
        {
            var vm = _taskModalVmFactory(this);

            UserTaskReadDto? dto = null;
            if (task != null)
            {
                dto = new UserTaskReadDto
                {
                    TaskId = task.TaskId,
                    Title = task.Title,
                    Type = task.Type,
                    Date = task.Date,
                    Description = task.Description,
                    Priority = task.Priority,
                    UserId = task.UserId
                };
            }

            vm.Initialize(dto);

            var modal = new TaskModal(vm);
            await Shell.Current.Navigation.PushModalAsync(modal);
        }

        [RelayCommand]
        public async Task Delete(TaskItemViewModel task)
        {
            if (task == null) return;

            bool success = await _taskService.DeleteTaskAsync(task.TaskId);

            if (success)
            {
                await LoadTasksAsync();
            }
        }
    }
}