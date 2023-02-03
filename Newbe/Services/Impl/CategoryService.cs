using Newbe.Models;
using Newbe.Models.RequestModels;
using Newbe.Models.ViewModels;

namespace Newbe.Services.Impl;

public class CategoryService:ICategoryService
{
    private readonly MasterDbContext _context;

    public CategoryService(MasterDbContext context)
    {
        _context = context;
    }

    public List<CategoryResponse> Get()
    {
        var listCategory = _context.Categories.Select(c => new CategoryResponse()
        {
            ID = c.ID,
            Name = c.Name,
        }).ToList();
        return listCategory;
    }

    public CategoryResponse CreateCategory(CreateCategoryRequest request)
    {
        var newCategory = new Category()
        {
            ID = Guid.NewGuid(),
            Name = request.Name
        };
        _context.Add(newCategory);
        _context.SaveChanges();
        return new CategoryResponse()
        {
            ID = newCategory.ID,
            Name = newCategory.Name
        };
    }

    public CategoryResponse EditCategory(EditCategoryRequest request)
    {
        var targetCategory = _context.Categories.FirstOrDefault(c => c.ID == request.ID);
        if (targetCategory == null)
        {
            throw new Exception("Not found category");
        }

        targetCategory.Name = request.Name;
        _context.SaveChanges();
        return new CategoryResponse()
        {
            ID = targetCategory.ID,
            Name = targetCategory.Name
        }; 
    }

    public CategoryResponse DeleteCategory(Guid id)
    {
        var targetCategory = _context.Categories.FirstOrDefault(c => c.ID == id);
        if (targetCategory == null)
        {
            throw new Exception("Not found category");
        }

        _context.Remove(targetCategory);
        _context.SaveChanges();
        return new CategoryResponse()
        {
            ID = targetCategory.ID,
            Name = targetCategory.Name
        }; 
    }
}