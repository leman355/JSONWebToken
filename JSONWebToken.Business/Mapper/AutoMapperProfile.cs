using AutoMapper;
using JSONWebToken.Business.DTO.RoleDtos;
using JSONWebToken.Business.DTO.UserDtos;
using JSONWebToken.Entities;

namespace JSONWebToken.Business.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserToAddDto, User>();
            CreateMap<User, UserToAddDto>();
            CreateMap<User, UserToListDto>();
            CreateMap<UserToUpdateDto, User>();
            CreateMap<User, UserToUpdateDto>();
            CreateMap<User, UserLoginDto>();

            CreateMap<Role, RoleToListDto>();
            CreateMap<Role, RoleToAddDto>();
            CreateMap<RoleToAddDto, Role>();
            CreateMap<RoleToUpdateDto, Role>();
            CreateMap<Role, RoleToUpdateDto>();
        }
    }
}
