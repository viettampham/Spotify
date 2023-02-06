using Microsoft.AspNetCore.Mvc;
using Newbe.Models.RequestModels;
using Newbe.Services;

namespace Newbe.Controllers;
[ApiController]
[Route("[controller]")]
public class RoleController:ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("get-role")]
    public IActionResult GetRole()
    {
        var listRole = _roleService.GetRole();
        return Ok(listRole);
    }

    [HttpPost("add-role")]
    public IActionResult CreateRole(CreateRoleRequest request)
    {
        var newRole = _roleService.CreateRole(request);
        return Ok(newRole);
    }

    [HttpPost("edit-role")]
    public IActionResult EditRole(EditRoleRequest request)
    {
        var targetRole = _roleService.EditRole(request);
        return Ok(targetRole);
    }

    [HttpDelete("delete-role")]
    public IActionResult DeleteRole(Guid id)
    {
        var targetRole = _roleService.DeleteRole(id);
        return Ok(targetRole);
    }
}
