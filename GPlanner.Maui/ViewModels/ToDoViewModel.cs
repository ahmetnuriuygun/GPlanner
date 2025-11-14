using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GPlanner.Core.Model;
using GPlanner.Maui.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace GPlanner.Maui.ViewModels
{
    public partial class ToDoViewModel : ObservableObject
    {
        private readonly IToDoService _toDoService;
        private readonly IGeminiService _geminiService;

        private readonly IScheduledTaskService _scheduledTaskService;
        private const int LoggedInUserId = 1;

        private List<DailyPlanItem> _allPlans = new();

        [ObservableProperty]
        private ObservableCollection<ScheduledTask> currentDayTasks = new();

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private DateTime currentDisplayDate = DateTime.Today;

        [ObservableProperty]
        private string lastGenerationDate = "Plan not yet generated.";

        public string TodayDateText => CurrentDisplayDate.ToString("ddd, dd MMM", CultureInfo.CurrentCulture);

        public ToDoViewModel(IToDoService toDoService, IGeminiService geminiService, IScheduledTaskService scheduledTaskService)
        {
            _toDoService = toDoService;
            _geminiService = geminiService;
            _scheduledTaskService = scheduledTaskService;
            Task.Run(async () => await LoadDailyPlansAsync());
        }


        [RelayCommand]
        public async Task LoadDailyPlansAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;

                var fetchedPlans = await _toDoService.GetDailyPlansAsync();

                if (fetchedPlans is not null)
                {
                    _allPlans = fetchedPlans.OrderBy(p => p.Date).ToList();

                    var latestPlan = _allPlans
                        .Where(p => p.LastGenerated.HasValue)
                        .OrderByDescending(p => p.LastGenerated)
                        .FirstOrDefault();
                    if (latestPlan?.LastGenerated.HasValue == true)
                    {
                        LastGenerationDate = latestPlan.LastGenerated.Value.ToLocalTime()
                                              .ToString("dd MMM yyyy, HH:mm");
                    }
                    else
                    {
                        LastGenerationDate = "Plan not yet generated.";
                    }

                    UpdateCurrentDayTasks(CurrentDisplayDate);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadDailyPlansAsync Error: {ex}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void UpdateCurrentDayTasks(DateTime date)

        {
            var targetPlan = _allPlans.FirstOrDefault(p => p.Date.Date == date.Date);

            if (targetPlan != null)
            {
                var sortedTasks = targetPlan.Tasks.OrderBy(t => t.StartTime).ToList();
                CurrentDayTasks = new ObservableCollection<ScheduledTask>(sortedTasks);
            }
            else
            {
                CurrentDayTasks.Clear();
            }

            OnPropertyChanged(nameof(TodayDateText));

        }


        [RelayCommand]
        private void NavigateDay(string directionString)
        {
            if (int.TryParse(directionString, out int direction))
            {
                CurrentDisplayDate = CurrentDisplayDate.AddDays(direction);
                UpdateCurrentDayTasks(CurrentDisplayDate);
            }
        }

        [RelayCommand]
        public async Task ToggleTaskStatus(int taskId)
        {

            var task = CurrentDayTasks.FirstOrDefault(t => t.Id == taskId);

            if (IsBusy || task == null) return;

            try
            {
                task.IsCompleted = !task.IsCompleted;

                bool success = await _scheduledTaskService.UpdateScheduledTaskAsync(taskId);
                if (!success)
                {
                    task.IsCompleted = !task.IsCompleted;
                    await Shell.Current.DisplayAlert("Error", "Could not update task completion status.", "OK");
                }

                CurrentDayTasks = new ObservableCollection<ScheduledTask>(CurrentDayTasks);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ToggleTaskStatus Error: {ex}");
                await Shell.Current.DisplayAlert("Error", "An unexpected error occurred while updating status.", "OK");
            }
        }

        [RelayCommand]
        public async Task GenerateAIPlanAsync()
        {
            System.Diagnostics.Debug.WriteLine("GenerateAIPlanAsync invoked.");
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                bool success = await _geminiService.GeneratePlanningAsync(LoggedInUserId);

                if (success)
                {
                    await LoadDailyPlansAsync();
                    await Shell.Current.DisplayAlert("Success", "New plan successfully generated.", "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Plan generation failed. You have reached maximum allowed requests. Check API status.", "OK");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GenerateAIPlan Error: {ex}");
                await Shell.Current.DisplayAlert("Critical Error", "An unexpected error occurred during plan generation.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}