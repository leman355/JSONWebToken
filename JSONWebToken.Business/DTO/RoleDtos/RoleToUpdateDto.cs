namespace JSONWebToken.Business.DTO.RoleDtos
{
    public record RoleToUpdateDto
    {
        public string RoleName { get; set; }
        public string Key { get; set; }
    }
}
