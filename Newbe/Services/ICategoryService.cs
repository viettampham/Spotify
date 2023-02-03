using Newbe.Models.RequestModels;
using Newbe.Models.ViewModels;

namespace Newbe.Services;

public interface ICategoryService
{
    List<CategoryResponse> Get();
    CategoryResponse CreateCategory(CreateCategoryRequest request);
    CategoryResponse EditCategory(EditCategoryRequest request);
    CategoryResponse DeleteCategory(Guid id);
}