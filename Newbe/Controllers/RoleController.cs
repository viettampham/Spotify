using Microsoft.AspNetCore.Mvc;
using Newbe.Models.RequestModels;
using Newbe.Models.ViewModels;
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
        return Ok(_roleService.GetRole());
    }

    [HttpPost("add-role")]
    public async Task<IActionResult> CreateRole(CreateRoleRequest request)
    {
        return Ok(await _roleService.CreateRole(request));
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
