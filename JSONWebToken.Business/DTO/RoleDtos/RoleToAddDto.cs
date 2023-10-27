namespace JSONWebToken.Business.DTO.RoleDtos
{
    public record RoleToAddDto
    {
        public string RoleName { get; set; }
        public string Key { get; set; }
    }
}
