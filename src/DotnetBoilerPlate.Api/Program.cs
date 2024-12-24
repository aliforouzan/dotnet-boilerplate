using DotnetBoilerPlate.Api.Configurations;
using DotnetBoilerPlate.Api.Controllers;
using DotnetBoilerPlate.Api.Filters.Res;
using DotnetBoilerPlate.Domain.Interfaces.Auth;
using DotnetBoilerPlate.Domain.Services.Auth;
using DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;
using DotnetBoilerPlate.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.AddValidationSetup();

builder.Services.AddAuthorization();

// Swagger
builder.Services.AddSwaggerSetup();

// Persistence
builder.Services.AddPersistenceSetup(builder.Configuration);

// Application layer setup
builder.Services.AddApplicationSetup();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        options.DefaultChallengeScheme =
            options.DefaultForbidScheme =
                options.DefaultScheme =
                    options.DefaultSignInScheme =
                        options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
        )
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("ManagerPolicy", policy => policy.RequireRole("Manager"));
    options.AddPolicy("EmployeePolicy", policy => policy.RequireRole("Employee"));
    options.AddPolicy("CustomerPolicy", policy => policy.RequireRole("Customer"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});

builder.Services.AddAuthorization();

// Application layer services

// Domain layer services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ILoginService, LoginService>();

// Persistence layer services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IProvinceRepository, ProvinceRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Request response compression
builder.Services.AddCompressionSetup();

// HttpContextAcessor
builder.Services.AddHttpContextAccessor();

// Mediator
builder.Services.AddMediatRSetup();

// Exception handler
builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Logging.ClearProviders();

// Add serilog
if (builder.Environment.EnvironmentName != "Testing")
{
    builder.Host.UseLoggingSetup(builder.Configuration);
    
    // Add opentelemetry
    builder.AddOpenTemeletrySetup();
}

// Mapster setup
MapsterSetup.AddMapsterSetup();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseResponseCompression();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseSwaggerSetup();
app.UseHsts();

app.UseResponseCompression();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// app.MapHeroEndpoints();
// app.MapGroup("api/identity")
//     .WithTags("Identity")
//     .MapIdentityApi<ApplicationUser>();

app.MapAuthEndpoints();
app.MapOrderEndpoints();
app.MapGeoEndpoints();

await app.RunAsync();