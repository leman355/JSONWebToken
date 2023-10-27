using JSONWebToken.DataAccess.Abstract;
using JSONWebToken.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSONWebToken.DataAccess.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.Include(x => x.Role).ToListAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users.Include(x => x.Role)
                .Where(u => u.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetUsersByRoleId(int roleId)
        {
            return await _context.Users.Include(x => x.Role)
                .Where(u => u.RoleId == roleId)
                .ToListAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserById(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.Include(x => x.Role)
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<Role> GetUserRoleByEmail(string email)
        {
            User user = await GetUserByEmail(email);
            return user?.Role;
        }
    }
}
