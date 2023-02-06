using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newbe.Models;
using Newbe.Services;
using Newbe.Services.Impl;
using Newbe.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//cau hinh DB
builder.Services.AddDbContextPool<MasterDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("BloggingDatabase")));
//add scope

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddScoped<ILovedSongService, LovedSongService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddAutoMapper(typeof(Program)); 
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<MasterDbContext>()
    .AddDefaultTokenProviders();
/*//add authen
builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience ="Spotify",
                    ValidIssuer = "Ptam123aA@",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DefaultApplication.SecretKey)),
                    ClockSkew = TimeSpan.Zero,
                };
                /*options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/image"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = async context =>
                    {
                        var newIdentity = new ClaimsIdentity();
                        var accountService = context.HttpContext.RequestServices.GetService<IAccountService>();
                        IEnumerable<Claim> claims = await accountService.GetClaimsByUser(context);
                        foreach (Claim claim in claims)
                        {
                            newIdentity.AddClaim(claim);
                        }
                        context.Principal.AddIdentity(newIdentity);
                    }
                };#1#
            });*/

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title ="BeSpotify", Version ="v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please input Token JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});



var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    // this defines a CORS policy called "default"
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options => options.AllowAnyOrigin());  

app.Run();