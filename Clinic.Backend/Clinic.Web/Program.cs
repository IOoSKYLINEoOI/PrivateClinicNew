using Clinic.DataAccess;
using Clinic.Application;
using Clinic.Infrastructure;
using Clinic.Web.Extensions;
using Serilog;
using Clinic.Infrastructure.Authentication;
using Clinic.Web.Infrastructure;
using Clinic.Web.Middlewares;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Подключение User Secrets
builder.Configuration.AddUserSecrets<Program>();

// Настройка Serilog
builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

var services = builder.Services;
var configuration = builder.Configuration;

// Регистрация аутентификации
builder.Services.AddApiAuthentication(builder.Configuration);

// Добавление контроллеров
builder.Services.AddControllers();

// Настройка Swagger для документации API
builder.Services.AddSwaggerGen();

// Настройка JWT и ролей
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.Configure<RolePermissionsOptions>(builder.Configuration.GetSection("RolePermissionsOptions"));

// Регистрация сервисов
services
    .AddPersistence(configuration)
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddProblemDetails();

// Создание и настройка приложения
var app = builder.Build();

// Middleware для обработки ошибок
app.UseMiddleware<GlobalExceptionHandler>(); // Переместите это наверх, перед другими middleware

// Middleware для логирования контекста запроса
app.UseMiddleware<RequestLogContextMiddleware>();


// Настройка среды разработки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Использование статических файлов
app.UseStaticFiles();

// Настройка маршрутизации
app.UseRouting();

app.UseCors(x =>
{
    x.AllowAnyHeader();
    x.WithOrigins("https://localhost:3000");
    x.AllowAnyMethod();
    x.AllowCredentials();
});

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

// Использование аутентификации и авторизации
app.UseAuthentication();
app.UseAuthorization();

// Логирование запросов
app.UseSerilogRequestLogging();

// Регистрация пользовательского middleware для логирования контекста запроса
app.UseMiddleware<RequestLogContextMiddleware>();

// Настройка маршрутов
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
