using Clinic.DataAccess;
using Clinic.Application;
using Clinic.Infrastructure;
using Clinic.Web.Extensions;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.Options;
using Serilog;
using Clinic.Infrastructure.Authentication;
using Clinic.Web.Infrastructure;
using Clinic.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

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

// Регистрация обработчика исключений
services.AddExceptionHandler<GlobalExceptionHandler>();

// Создание и настройка приложения
var app = builder.Build();

// Настройка среды разработки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Использование статических файлов
app.UseStaticFiles();

// Конфигурация политики cookie
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

// Использование аутентификации и авторизации
app.UseAuthentication();
app.UseAuthorization();

// Настройка обработки исключений
app.UseExceptionHandler("/error");

// Логирование запросов
app.UseSerilogRequestLogging();

// Настройка маршрутизации
app.UseRouting();

// Регистрация пользовательского middleware для логирования контекста запроса
app.UseMiddleware<RequestLogContextMiddleware>();

// Настройка маршрутов
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();













//app.UseCors(x =>
//{
//    x.WithHeaders().AllowAnyHeader();
//    x.WithOrigins("http://localhost:3000");
//    x.WithMethods().AllowAnyMethod();
//});

//// Регистрация политик после настройки сервисов и перед запуском приложения
//using (var scope = app.Services.CreateScope())
//{
//    var policyProvider = scope.ServiceProvider.GetRequiredService<PolicyProvider>();
//    var authorizationOptions = scope.ServiceProvider.GetRequiredService<IOptions<Microsoft.AspNetCore.Authorization.AuthorizationOptions>>().Value;
//    policyProvider.RegisterPolicies(authorizationOptions);
//}


