﻿using Newbe.Models.RequestModels;
using Newbe.Models.ViewModels;

namespace Newbe.Services;

public interface IUserService
{
    Task<LoginResponse> Login(LoginRequest request);
    Task<string> Registration(RegistrationUser request);
    List<UserResponse> GetlistUsers();
    UserResponse DeleteUser(Guid id);
}