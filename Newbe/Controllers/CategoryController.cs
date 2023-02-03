using Microsoft.AspNetCore.Mvc;
using Newbe.Models.RequestModels;
using Newbe.Services;

namespace Newbe.Controllers;
[ApiController]
[Route("[controller]")]
public class CategoryController:ControllerBase
{
    public readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("get-list-category")]
    public IActionResult GetListCategory()
    {
        var listCate = _categoryService.Get();
        return Ok(listCate);
    }

    [HttpPost("create-category")]
    public IActionResult CreateCategory(CreateCategoryRequest request)
    {
        var newCategory = _categoryService.CreateCategory(request);
        return Ok(newCategory);
    }

    [HttpPost("edit-category")]
    public IActionResult EditCategory(EditCategoryRequest request)
    {
        var targetrCategory = _categoryService.EditCategory(request);
        return Ok(targetrCategory);
    }
    
    [HttpDelete("delete-category")]
    public IActionResult Delete(Guid id)
    {
        var targetrCategory = _categoryService.DeleteCategory(id);
        return Ok(targetrCategory);
    }
}