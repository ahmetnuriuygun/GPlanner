using GPlanner.Core.Model;
public interface IUserRepository
{
    Task<User> GetUserByIdAsync(int userId);
}