using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GPlanner.Core.Model;
using GPlanner.Maui.Services.Dtos;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using GPlanner.Maui.Interfaces;
using AutoMapper;
using GPlanner.Maui.Pages;
using System.Collections.Generic;

namespace GPlanner.Maui.ViewModels
{
    public partial class TaskItemViewModel : ObservableObject
    {
        private readonly TasksViewModel _parent;

        public TaskItemViewModel(UserTaskReadDto dto, TasksViewModel parent)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));

            TaskId = dto.TaskId;
            Title = dto.Title;
            Type = dto.Type;
            Date = dto.Date;
            Description = dto.Description;
            Priority = dto.Priority;
            UserId = dto.UserId;
        }

        public int TaskId { get; }
        public string Title { get; }
        public TaskType Type { get; }
        public DateTime Date { get; }

        public string Description { get; }
        public int Priority { get; }
        public int UserId { get; }

        [RelayCommand]
        private async Task Edit()
        {
            await _parent.OpenTaskModal(this);
        }

        [RelayCommand]
        private async Task Delete()
        {
            await _parent.Delete(this);
        }


    }
}
