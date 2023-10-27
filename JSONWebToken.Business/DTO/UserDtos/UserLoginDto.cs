namespace JSONWebToken.Business.DTO.UserDtos
{
    public record UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
