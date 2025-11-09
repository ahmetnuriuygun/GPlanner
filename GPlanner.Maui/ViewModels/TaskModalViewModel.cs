using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GPlanner.Core.Model;
using GPlanner.Maui.Interfaces;
using GPlanner.Maui.Services.Dtos;
using AutoMapper;

namespace GPlanner.Maui.ViewModels
{
    public partial class TaskModalViewModel : ObservableObject
    {
        private readonly IUserTaskService _taskService;
        private readonly IMapper _mapper;

        [ObservableProperty]
        private bool isEditMode;

        [ObservableProperty]
        private UserTaskBaseDto activeTask = null!;

        [ObservableProperty]
        private TimeSpan newTaskTime = DateTime.Now.TimeOfDay;

        public Array TaskTypes => Enum.GetValues(typeof(TaskType));

        public TaskModalViewModel(IUserTaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        public void Initialize(UserTaskReadDto? dto)
        {
            if (dto != null)
            {
                IsEditMode = true;
                ActiveTask = _mapper.Map<UserTaskUpdateDto>(dto);
            }
            else
            {
                IsEditMode = false;
                ActiveTask = new UserTaskCreateDto { Date = DateTime.Now.Date };
            }
        }

        [RelayCommand]
        public async Task AddOrSaveAsync()
        {
            ActiveTask.Date = ActiveTask.Date.Date + NewTaskTime;

            if (IsEditMode)
            {
                var entity = _mapper.Map<UserTask>((UserTaskUpdateDto)ActiveTask);
                await _taskService.UpdateTaskAsync(entity);
            }
            else
            {
                var dto = (UserTaskCreateDto)ActiveTask;
                dto.UserId = 1;
                var entity = _mapper.Map<UserTask>(dto);
                await _taskService.CreateTaskAsync(entity);
            }

            await Shell.Current.Navigation.PopModalAsync();
        }

        [RelayCommand]
        async Task CancelAsync()
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}
