using JSONWebToken.Entities;

namespace JSONWebToken.Business.DTO.UserDtos
{
    public record UserToListDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public bool IsDeleted { get; set; }
        public Role Role { get; set; }
    }
}
