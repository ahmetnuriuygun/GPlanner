using GPlanner.Core.Model;
using System.Threading.Tasks;
public interface IUserService
{
    Task<User> GetUserByIdAsync(int id);
}