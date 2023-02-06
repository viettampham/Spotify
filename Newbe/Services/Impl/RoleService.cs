using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Newbe.Models;
using Newbe.Models.RequestModels;
using Newbe.Models.ViewModels;

namespace Newbe.Services.Impl;

public class RoleService:IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly MasterDbContext _context;
    private readonly IMapper _mapper;

    public RoleService(RoleManager<ApplicationRole> roleManager, MasterDbContext context, IMapper mapper)
    {
        _roleManager = roleManager;
        _context = context;
        _mapper = mapper;
    }

    public List<RoleResponse> GetRole()
    {
        var listRole = _context.Roles.Select(r => new RoleResponse()
        {
            RoleID = r.Id,
            Name = r.Name
        }).ToList();
        return listRole;
    }

    public async Task<RoleResponse> CreateRole(CreateRoleRequest request)
    {
        var newRole = new ApplicationRole()
        {
            Id = Guid.NewGuid(),
            Name = request.RoleName
        };
        await _roleManager.CreateAsync(newRole);
        await _context.SaveChangesAsync();
        return new RoleResponse()
        {
            RoleID = newRole.Id,
            Name = newRole.Name
        };
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
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}