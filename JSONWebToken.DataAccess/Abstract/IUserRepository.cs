using JSONWebToken.Entities;

namespace JSONWebToken.DataAccess.Abstract
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int userId);
        Task<List<User>> GetUsersByRoleId(int roleId);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task DeleteUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<Role> GetUserRoleByEmail(string email);
    }
}
