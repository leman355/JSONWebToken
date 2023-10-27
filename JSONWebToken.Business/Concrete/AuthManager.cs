using AutoMapper;
using JSONWebToken.Business.Abstract;
using JSONWebToken.Business.DTO.UserDtos;
using JSONWebToken.DataAccess.Abstract;
using JSONWebToken.Entities;
using System.Security.Cryptography;
using System.Text;

namespace JSONWebToken.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthManager(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserLoginDto> Login(UserLoginDto dto)
        {
            var user = await _userRepository.GetUserByEmail(dto.Email);

            if (user == null || !VerifyPassword(dto.Password, user.Password))
            {
                return null;
            }

            return _mapper.Map<UserLoginDto>(user);
        }

        public async Task<UserToAddDto> Register(UserToAddDto dto)
        {
            if (await _userRepository.GetUserByEmail(dto.Email) != null)
            {
                return null;
            }

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

        private bool VerifyPassword(string password, string storedHash)
        {
            string hashedPassword = HashPassword(password);
            return string.Equals(hashedPassword, storedHash, StringComparison.OrdinalIgnoreCase);
        }

        public async Task<UserToListDto> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            return _mapper.Map<UserToListDto>(user);
        }

        public async Task<string> GetUserRoleByEmail(string email)
        {
            var role = await _userRepository.GetUserRoleByEmail(email);
            if (role == null)
            {
                return null;
            }

            return role.Key;
        }
    }
}