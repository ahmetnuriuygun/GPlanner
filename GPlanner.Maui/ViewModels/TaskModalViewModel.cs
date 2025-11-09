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
        private readonly IMapper _mapper;
        private readonly TasksViewModel _parentViewModel;

        [ObservableProperty]
        private bool isEditMode;

        [ObservableProperty]
        private UserTaskUpdateDto activeTask = new UserTaskUpdateDto();

        [ObservableProperty]
        private TimeSpan newTaskTime = DateTime.Now.TimeOfDay;

        public Array TaskTypes => Enum.GetValues(typeof(TaskType));

        public TaskModalViewModel(IUserTaskService taskService, IMapper mapper, TasksViewModel parentViewModel)
        {
            _taskService = taskService;
            _mapper = mapper;
            _parentViewModel = parentViewModel;
        }

        public void Initialize(UserTaskReadDto? dto)
        {
            if (dto != null)
            {
                IsEditMode = true;
                ActiveTask = _mapper.Map<UserTaskUpdateDto>(dto);
                NewTaskTime = dto.Date.TimeOfDay;
            }
            else
            {
                IsEditMode = false;
                ActiveTask = new UserTaskUpdateDto { UserId = 1, Date = DateTime.Now.Date };
                NewTaskTime = DateTime.Now.TimeOfDay;
            }
        }

        [RelayCommand]
        public async Task AddOrSaveAsync()
        {
            ActiveTask.Date = ActiveTask.Date.Date + NewTaskTime;

            bool success = false;

            try
            {
                if (IsEditMode)
                {

                    var entity = _mapper.Map<UserTask>(ActiveTask);

                    if (entity.TaskId == 0)
                    {
                        System.Diagnostics.Debug.WriteLine("Update failed: TaskId is 0. Data flow error.");
                        await Shell.Current.DisplayAlert("Error", "Cannot save task: Task ID is missing.", "OK");
                        return;
                    }
                    success = await _taskService.UpdateTaskAsync(entity);
                }
                else
                {

                    var createDto = _mapper.Map<UserTaskCreateDto>(ActiveTask);
                    createDto.UserId = 1;
                    var entity = _mapper.Map<UserTask>(createDto);
                    success = await _taskService.CreateTaskAsync(entity);
                }

                if (success)
                {
                    await _parentViewModel.LoadTasksAsync();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Task could not be saved. Check API connection.", "OK");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error during AddOrSaveAsync: {ex}");
                await Shell.Current.DisplayAlert("Fatal Error", "An unexpected error occurred while saving.", "OK");
            }
            finally
            {
                await Shell.Current.Navigation.PopModalAsync();
            }
        }

        [RelayCommand]
        async Task CancelAsync()
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}