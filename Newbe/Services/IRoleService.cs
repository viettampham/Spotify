using Newbe.Models.RequestModels;
using Newbe.Models.ViewModels;

namespace Newbe.Services;

public interface IRoleService
{
    List<RoleResponse> GetRole(); 
    Task<RoleResponse> CreateRole(CreateRoleRequest request);
    RoleResponse EditRole(EditRoleRequest request);
    bool DeleteRole(Guid id);
    

}