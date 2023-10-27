using AutoMapper;
using JSONWebToken.Business.Abstract;
using JSONWebToken.Business.DTO.UserDtos;
using JSONWebToken.DataAccess.Abstract;
using JSONWebToken.Entities;
using System.Security.Cryptography;
using System.Text;

namespace JSONWebToken.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserManager(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserToListDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return _mapper.Map<List<UserToListDto>>(users);
        }

        public async Task<UserToListDto> GetUserById(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            return _mapper.Map<UserToListDto>(user);
        }

        public async Task<List<UserToListDto>> GetUsersByRoleId(int roleId)
        {
            var users = await _userRepository.GetUsersByRoleId(roleId);
            return _mapper.Map<List<UserToListDto>>(users);
        }

        public async Task<UserToAddDto> CreateUser(UserToAddDto dto)
        {
          

            var user = _mapper.Map<User>(dto);

            user.Password = HashPassword(dto.Password);

            var createdUser = await _userRepository.CreateUser(user);

            return _mapper.Map<UserToAddDto>(createdUser);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public async Task<UserToUpdateDto> UpdateUser(int userId, UserToUpdateDto dto)
        {
            var existingUser = await _userRepository.GetUserById(userId);

            if (existingUser == null)
            {
                return null;
            }

            _mapper.Map(dto, existingUser);

            if (!string.IsNullOrEmpty(dto.Password))
            {
                existingUser.Password = HashPassword(dto.Password);
            }

            var updatedUser = await _userRepository.UpdateUser(existingUser);
            return _mapper.Map<UserToUpdateDto>(updatedUser);
        }

        public async Task DeleteUserById(int userId)
        {
            await _userRepository.DeleteUserById(userId);
        }

        public async Task<Role> GetUserRoleByEmail(string email)
        {
            return await _userRepository.GetUserRoleByEmail(email);
        }
    }
}
