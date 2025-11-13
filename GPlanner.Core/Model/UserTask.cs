using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace GPlanner.Core.Model
{
    public partial class UserTask : ObservableObject
    {

        public int TaskId { get; set; }



        public int UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public TaskType Type { get; set; }

        public DateTime Date { get; set; }

        public int Priority { get; set; }

        public bool IsArchived { get; set; } = false;



        public UserTask()
        {
        }
    }
}
