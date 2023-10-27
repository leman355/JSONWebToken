using AutoMapper;
using JSONWebToken.Business.Abstract;
using JSONWebToken.Business.DTO.RoleDtos;
using JSONWebToken.DataAccess.Abstract;
using JSONWebToken.Entities;

namespace JSONWebToken.Business.Concrete
{
    public class RoleManager : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleManager(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<RoleToListDto>> GetAllRoles()
        {
            List<Role> roles = await _roleRepository.GetAllRoles();
            List<RoleToListDto> dtos = _mapper.Map<List<RoleToListDto>>(roles);
            return dtos;
        }

        public async Task<RoleToListDto> GetRoleById(int roleId)
        {
            var role = await _roleRepository.GetRoleById(roleId);
            return _mapper.Map<RoleToListDto>(role);
        }

        public async Task<RoleToAddDto> CreateRole(RoleToAddDto dto)
        {
            Role role = _mapper.Map<Role>(dto);
            var createdRole = await _roleRepository.CreateRole(role);

            return _mapper.Map<RoleToAddDto>(createdRole);
        }

        public async Task<RoleToUpdateDto> UpdateRole(int roleId, RoleToUpdateDto roleToUpdateDto)
        {
            Role existingRole = await _roleRepository.GetRoleById(roleId);

            if (existingRole == null)
            {
                return null;
            }

            _mapper.Map(roleToUpdateDto, existingRole);
            var updatedRole = await _roleRepository.UpdateRole(existingRole);

            return _mapper.Map<RoleToUpdateDto>(updatedRole);
        }

        public async Task DeleteRoleById(int roleId)
        {
            await _roleRepository.DeleteRoleById(roleId);
        }
    }
}
