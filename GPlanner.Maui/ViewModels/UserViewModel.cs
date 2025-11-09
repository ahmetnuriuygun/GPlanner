using CommunityToolkit.Mvvm.ComponentModel;
using GPlanner.Core.Model;

namespace GPlanner.Maui.ViewModels
{
    public partial class UserViewModel : ObservableObject
    {
        [ObservableProperty]
        User user;

        public UserViewModel()
        {
            User = new User("Ahmet's Mock Profile");
        }

    }
}