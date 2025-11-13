using CommunityToolkit.Mvvm.ComponentModel;
using GPlanner.Core.Model;

namespace GPlanner.Maui.ViewModels
{
    public partial class UserViewModel : ObservableObject
    {

        private readonly IUserService _userService;
        private readonly int loggedInUserId = 1;

        [ObservableProperty]
        private User user = new User();

        public UserViewModel(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task LoadUserAsync()
        {

            var loadedUser = await _userService.GetUserByIdAsync(loggedInUserId);
            User = loadedUser;
        }

    }
}