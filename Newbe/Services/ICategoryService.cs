using Newbe.Models.RequestModels;
using Newbe.Models.ViewModels;

namespace Newbe.Services;

public interface ICategoryService
{
    List<CategoryResponse> Get();
    MessageResponse CreateCategory(CreateCategoryRequest request);
    MessageResponse EditCategory(EditCategoryRequest request);
    MessageResponse DeleteCategory(Guid id);
}