using FluentValidation;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Events;
using System.Text;
using TI_API.Application.Common.Interfaces;
using TI_API.Application.Common.Mappings;
using TI_API.Application.Common.Settings;
using TI_API.Application.Services;
using TI_API.Domain.Entities;
using TI_API.Entities;
using TI_API.Infraestucture;
using TI_API.Infraestucture.Persistence;
using TI_API.Infraestucture.Persistence.Seedings;
using TI_API.Infraestucture.Repositories;
using TI_API.Infraestucture.Services;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("log.txt")
    .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Registrar UnitOfWorks
builder.Services.AddScoped<IUnitOfWorks, UnitOfWorks>();

// Si quieres registrar los repositorios individualmente (opcional)
builder.Services.AddScoped<IIndicadorRepository, IndicadorRepository>();
builder.Services.AddScoped<IProcesoRepository, ProcesoRepository>();
builder.Services.AddScoped<IObjetivoRepository, ObjetivoRepository>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IIndicadorDeAreaRepository, IndicadorDeAreaRepository>();
builder.Services.AddScoped<IIndicadorDeObjetivoRepository, IndicadorDeObjetivoRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configuración de Email
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailSender, EmailSender>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// 1. Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 2. Configuración de la Base de Datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseNpgsql(connectionString));

builder.Services.Configure<IdentitySettings>(builder.Configuration.GetSection("IdentitySettings"));

var identitySettings = builder.Configuration.GetSection("IdentitySettings").Get<IdentitySettings>()!;


builder.Services.AddIdentity<ApplicationUser, ApplicationRol>(options =>
{
    // Password settings
    options.Password.RequireDigit = identitySettings.Password.RequireDigit;
    options.Password.RequireLowercase = identitySettings.Password.RequireLowercase;
    options.Password.RequireUppercase = identitySettings.Password.RequireUppercase;
    options.Password.RequireNonAlphanumeric = identitySettings.Password.RequireNonAlphanumeric;
    options.Password.RequiredLength = identitySettings.Password.RequiredLength;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identitySettings.Lockout.DefaultLockoutTimeSpanInMinutes);
    options.Lockout.MaxFailedAccessAttempts = identitySettings.Lockout.MaxFailedAccessAttempts;
    options.Lockout.AllowedForNewUsers = identitySettings.Lockout.AllowedForNewUsers;

    // User settings
    options.User.RequireUniqueEmail = identitySettings.User.RequireUniqueEmail;

    // SignIn settings
    options.SignIn.RequireConfirmedEmail = identitySettings.SignIn.RequireConfirmedEmail;
})
.AddEntityFrameworkStores<CommandContext>()
.AddDefaultTokenProviders();

// Contextos
builder.Services.AddDbContext<CommandContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<QueryContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servicios
builder.Services.AddScoped<IQueryContext>(provider => provider.GetRequiredService<QueryContext>());
builder.Services.AddScoped<ISecurityService, SecurityService>();


// Configurar JwtSettings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Registrar el servicio de generación de tokens
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

// Configuración de autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("JefeProcesoOnly", policy => policy.RequireRole("JefeProceso"));
    options.AddPolicy("JefeAreaOnly", policy => policy.RequireRole("JefeArea"));
    options.AddPolicy("UsuarioNormalOnly", policy => policy.RequireRole("UsuarioNormal"));

    // Políticas combinadas
    options.AddPolicy("AdminOrJefeProceso", policy =>
        policy.RequireRole("Admin", "JefeProceso"));
    options.AddPolicy("AdminOrJefeArea", policy =>
        policy.RequireRole("Admin", "JefeArea"));
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 5. Configuración de Autorización
builder.Services.AddAuthorization();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbSeeder.SeedRolesAndAdminAsync(services);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseCors("AllowAngularApp");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
