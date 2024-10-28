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

// ����������� User Secrets
builder.Configuration.AddUserSecrets<Program>();

// ��������� Serilog
builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

var services = builder.Services;
var configuration = builder.Configuration;

// ����������� ��������������
builder.Services.AddApiAuthentication(builder.Configuration);

// ���������� ������������
builder.Services.AddControllers();

// ��������� Swagger ��� ������������ API
builder.Services.AddSwaggerGen();

// ��������� JWT � �����
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.Configure<RolePermissionsOptions>(builder.Configuration.GetSection("RolePermissionsOptions"));

// ����������� ��������
services
    .AddPersistence(configuration)
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddProblemDetails();

// �������� � ��������� ����������
var app = builder.Build();

// Middleware ��� ��������� ������
app.UseMiddleware<GlobalExceptionHandler>(); // ����������� ��� ������, ����� ������� middleware

// Middleware ��� ����������� ��������� �������
app.UseMiddleware<RequestLogContextMiddleware>();


// ��������� ����� ����������
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ������������� ����������� ������
app.UseStaticFiles();

// ��������� �������������
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

// ������������� �������������� � �����������
app.UseAuthentication();
app.UseAuthorization();

// ����������� ��������
app.UseSerilogRequestLogging();

// ����������� ����������������� middleware ��� ����������� ��������� �������
app.UseMiddleware<RequestLogContextMiddleware>();

// ��������� ���������
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
