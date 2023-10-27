namespace JSONWebToken.Business.DTO.UserDtos
{
    public record UserToAddDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public int RoleId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
