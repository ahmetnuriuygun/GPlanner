using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GPlanner.Core.Model;
using GPlanner.Maui.Pages;
using AutoMapper;
using GPlanner.Maui.Services.Dtos;
using GPlanner.Maui.Interfaces;

namespace GPlanner.Maui.ViewModels
{
    public partial class TasksViewModel : ObservableObject
    {
        private readonly IUserTaskService _taskService;
        private readonly IMapper _mapper;
        private readonly Func<TaskModalViewModel> _taskModalVmFactory;

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        ObservableCollection<UserTaskReadDto> tasks = new();

        [ObservableProperty]
        ObservableCollection<UserTaskReadDto> filteredTasks = new();

        [ObservableProperty]
        UserTaskReadDto? currentTask;



        public Array TaskTypes => Enum.GetValues(typeof(TaskType));

        public TasksViewModel(IUserTaskService taskService, IMapper mapper, Func<TaskModalViewModel> taskModalVmFactory)
        {
            _taskService = taskService;
            _mapper = mapper;
            _taskModalVmFactory = taskModalVmFactory;

            Task.Run(async () => await LoadTasksAsync());
        }


        [RelayCommand]
        public async Task LoadTasksAsync()
        {
            int loggedInUserId = 1;
            var dtoList = await _taskService.GetTasksByUserIdAsync(loggedInUserId);
            var displayList = _mapper.Map<List<UserTaskReadDto>>(dtoList);

            Tasks.Clear();
            foreach (var item in displayList)
                Tasks.Add(item);

            OnSearchTextChanged(SearchText);
        }

        partial void OnSearchTextChanged(string value)
        {
            if (Tasks == null) return;

            if (string.IsNullOrWhiteSpace(value))
                FilteredTasks = new ObservableCollection<UserTaskReadDto>(Tasks);
            else
            {
                var results = Tasks
                    .Where(t => t.Title.Contains(value, StringComparison.OrdinalIgnoreCase));
                FilteredTasks = new ObservableCollection<UserTaskReadDto>(results);
            }
        }

        [RelayCommand]
        public async Task OpenTaskModal(UserTaskReadDto? task = null)
        {
            var vm = _taskModalVmFactory();
            vm.Initialize(task);

            var modal = new TaskModal(vm);
            await Shell.Current.Navigation.PushModalAsync(modal);
        }





        [RelayCommand]
        public async Task Delete(UserTaskReadDto task)
        {
            await _taskService.DeleteTaskAsync(task.TaskId);
            await LoadTasksAsync();
        }



    }
}