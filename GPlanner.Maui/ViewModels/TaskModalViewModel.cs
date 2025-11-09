using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GPlanner.Core.Model;

using GPlanner.Maui.Models;
namespace GPlanner.Maui.ViewModels
{



    public partial class TaskModalViewModel : ObservableObject
    {


        [ObservableProperty]
        TaskDisplayModel currentTask;


        [ObservableProperty]
        TimeSpan newTaskTime;

        [ObservableProperty]
        bool isEditMode;

        public Array TaskTypes => Enum.GetValues(typeof(TaskType));

        private TasksViewModel _parentVm;

        public void Initialize(UserTask task, TasksViewModel parentVm, bool isEdit = false)
        {

        }

        [RelayCommand]
        async Task AddOrSave()
        {

        }



        [RelayCommand]
        async Task Cancel()
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}