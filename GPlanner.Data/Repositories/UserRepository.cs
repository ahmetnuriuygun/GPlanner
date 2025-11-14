using GPlanner.Core.Model;
using GPlanner.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using GPlanner.Data;
namespace GPlanner.Data.Repositories
{
    public class UserRepository : IUserRepository

    {
        private readonly GPlannerDbContext _context;

        public UserRepository(GPlannerDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId) ?? new User();
        }
    }
}