using Newbe.Models.RequestModels;
using Newbe.Models.ViewModels;

namespace Newbe.Services;

public interface IUserService
{
    Task<LoginResponse> Login(LoginRequest request);
    Task<MessageResponse> Registration(RegistrationUser request);
    List<UserResponse> GetlistUsers();
    MessageResponse DeleteUser(Guid id);
}