using Microsoft.AspNetCore.Identity;
using Newbe.Models;
using Newbe.Models.RequestModels;
using Newbe.Models.ViewModels;

namespace Newbe.Services.Impl;

public class RoleService:IRoleService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly MasterDbContext _context;
    
    public List<RoleResponse> GetRole()
    {
        var listRole = _context.Roles.Select(r => new RoleResponse()
        {
            RoleID = r.Id,
            Name = r.Name
        }).ToList();
        return listRole;
    }

    public bool CreateRole(CreateRoleRequest request)
    {
        var newRole = new ApplicationRole()
        {
            Id = Guid.NewGuid(),
            Name = request.RoleName
        };
        _context.Add(newRole);
        _context.SaveChanges();
        return true;
    }

    public RoleResponse EditRole(EditRoleRequest request)
    {
        var targetRole = _context.Roles.FirstOrDefault(r => r.Id == request.RoleID);
        if (targetRole == null)
        {
            throw new Exception("Not found");
        }

        targetRole.Name = request.RoleName;
        _context.SaveChanges();
        return new RoleResponse()
        {
            RoleID = targetRole.Id,
            Name = targetRole.Name
        };
    }

    public bool DeleteRole(Guid id)
    {
        var targetRole = _context.Roles.FirstOrDefault(r => r.Id == id);
        if (targetRole == null)
        {
            throw new Exception("not found");
        }
        
        if (targetRole != null)
        {
            _context.Remove(targetRole);
            return true;
        }
        return false;
    }
}