using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newbe.Models;
using Newbe.Models.RequestModels;
using Newbe.Models.ViewModels;
using Newbe.Settings;

namespace Newbe.Services.Impl;

public class UserService:IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly MasterDbContext _context;

    public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration, MasterDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _context = context;
    }
    
    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var User = await _userManager.FindByNameAsync(request.UserName);
        if (User==null)
        {
            throw new Exception("User not exist");
        }

        var loginResponse = await _userManager.CheckPasswordAsync(User, request.PassWord);
        if (!loginResponse)
        {
            throw new Exception("Emai or PassWord not correct");
        }
        var token = await GenerateTokenJWTByUser(User);
        return new LoginResponse
        {   
            token = new JwtSecurityTokenHandler().WriteToken(token)
        };
    }

    public async Task<string> Registration(RegistrationUser request)
    {
        
        
        
        
        
        
        bool checkPvh = false;
        bool checkPvt = false;
        bool checkPnumber = false;
        bool checkPspecial = false;
        
        bool checkusername = false;
        string Equal = "@gmail.com";

        var checkUser = _context.Users.FirstOrDefault(u => u.UserName == request.UserName);
        if (checkUser != null)
        {
            return "UserName đã tồn tại !";
        }
        
        if (request.UserName.Contains(Equal))
        {
            checkusername = true;
        }

        if (checkusername == false)
        {
            return "UserName phải có '@gmail.com'";
        }

        for (var i = 0; i < request.PassWord.Length; i++)
        {
            if (request.PassWord[i] >= 'A' && request.PassWord[i] <= 'Z')
            {
                checkPvh = true;
            }

            if ((request.PassWord[i] >= 'a' && request.PassWord[i] <='z'))
            {
                checkPvt = true;
            }

            if ((request.PassWord[i] >= '0' && request.PassWord[i] <= '9' ))
            {
                checkPnumber = true;
            }
            
            if (request.PassWord[i] == '@')
            {
                checkPspecial = true;
            }
        }
        

        if (checkPvh == false)
        {
            return "Pasword phải chứa kí tự viết hoa";
        }

        if (checkPvt == false)
        {
            return "Password phải chứa kí tự viết thường";
        }

        if (checkPnumber == false)
        {
            return "Password phải chứa kí tự số";
        }

        if (checkPspecial == false)
        {
            return "Password phải chứa kí tự đặc biệt";
        }
        
        var newUser = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = request.UserName,
        };
        
        var newPassword = await _userManager.CreateAsync(newUser, request.PassWord);
        
        if (newPassword.Succeeded)
        {
            if (request.RoleName == "")
            {
                await _userManager.AddToRoleAsync(newUser,"user");
            }
            else
            {
                var targetRole = _context.Roles.FirstOrDefault(r => r.Name == request.RoleName);
                if (targetRole == null)
                {
                    throw new Exception("not found this role");
                }
                await _userManager.AddToRoleAsync(newUser,targetRole.Name);
            }
            return "Đăng kí thành công";
        }
        return "Đăng kí thất bại";
    }

    public List<UserResponse> GetlistUsers()
    {
        var listUser = _context.Users.Select(user => new UserResponse
        {
            id = user.Id,
            UserName = user.UserName,
        }).ToList();
        return listUser;
    }

    public UserResponse DeleteUser(Guid id)
    {
        var targetUser = _context.Users.FirstOrDefault(user => user.Id == id);
        if (targetUser==null)
        {
            throw new Exception("User not found");
        }

        _context.Remove(targetUser);
        _context.SaveChanges();
        return new UserResponse
        {
            id = targetUser.Id,
            UserName = targetUser.UserName,
        };
    }
    
    private async Task<JwtSecurityToken> GenerateTokenJWTByUser(ApplicationUser user)
    {
        var authClaims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (string role in userRoles)
        {
            var roleData = await _roleManager.FindByNameAsync(role);
        }
        authClaims.Add(new Claim(ClaimTypes.Role, "manyRole"));

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DefaultApplication.SecretKey));// key này phải trùng với key trong hàm Jwtbearer ở trong program.cs nhé cu

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(24),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

        return token;
    }
}