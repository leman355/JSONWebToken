namespace JSONWebToken.Business.DTO.RoleDtos
{
    public record RoleToListDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Key { get; set; }
    }
}
