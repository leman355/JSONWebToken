using JSONWebToken.Business.DTO.UserDtos;
using JSONWebToken.Entities;

namespace JSONWebToken.Business.Abstract
{
    public interface IUserService
    {
        Task<List<UserToListDto>> GetAllUsers();
        Task<UserToListDto> GetUserById(int userId);
        Task<List<UserToListDto>> GetUsersByRoleId(int roleId);
        Task<UserToAddDto> CreateUser(UserToAddDto dto);
        Task<UserToUpdateDto> UpdateUser(int id, UserToUpdateDto user);
        Task DeleteUserById(int userId);
        Task<Role> GetUserRoleByEmail(string email);
    }
}
