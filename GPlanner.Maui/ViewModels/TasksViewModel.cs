using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GPlanner.Core.Model;
using GPlanner.Maui.Pages;
using AutoMapper;
using GPlanner.Maui.Models;
using GPlanner.Maui.Interfaces;

namespace GPlanner.Maui.ViewModels
{
    public partial class TasksViewModel : ObservableObject
    {
        private readonly IUserTaskService _taskService;
        private readonly IMapper _mapper;

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        ObservableCollection<TaskDisplayModel> tasks = new();

        [ObservableProperty]
        ObservableCollection<TaskDisplayModel> filteredTasks = new();

        [ObservableProperty]
        TaskDisplayModel currentTask;



        public Array TaskTypes => Enum.GetValues(typeof(TaskType));

        public TasksViewModel(IUserTaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;

            LoadTasksAsync();
        }


        [RelayCommand]
        public async Task LoadTasksAsync()
        {
            int loggedInUserId = 1;

            var dtoList = await _taskService.GetTasksByUserIdAsync(loggedInUserId);

            var displayList = _mapper.Map<List<TaskDisplayModel>>(dtoList);

            Tasks.Clear();
            foreach (var item in displayList)
            {
                Tasks.Add(item);
            }

            OnSearchTextChanged(SearchText);
        }

        partial void OnSearchTextChanged(string value)
        {
            if (Tasks == null) return;

            if (string.IsNullOrWhiteSpace(value))
            {
                FilteredTasks = new ObservableCollection<TaskDisplayModel>(Tasks);
            }
            else
            {
                var results = Tasks
                    .Where(t => t.Title.Contains(value, StringComparison.OrdinalIgnoreCase) ||
                                t.Description.Contains(value, StringComparison.OrdinalIgnoreCase));

                FilteredTasks = new ObservableCollection<TaskDisplayModel>(results);
            }
        }

        [RelayCommand]
        public async Task OpenTaskModal(TaskDisplayModel task = null)
        {

        }



        [RelayCommand]
        public async Task Delete(TaskDisplayModel task)
        {

        }

        [RelayCommand]
        async Task EditTask(TaskDisplayModel task)
        {

        }

        public void UpdateTaskList(TaskDisplayModel updatedTask, bool isNew)
        {

        }
    }
}